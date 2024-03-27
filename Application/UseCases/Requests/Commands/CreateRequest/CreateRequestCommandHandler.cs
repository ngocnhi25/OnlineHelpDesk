using Application.Common.Messaging;
using Application.DTOs.Notificarions;
using Application.DTOs.Requests;
using Application.Services;
using AutoMapper;
using Domain.Entities.Notifications;
using Domain.Entities.Requests;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Requests.Commands.CreateRequest
{
    public sealed class CreateRequestCommandHandler : ICommandHandler<CreateRequestCommand>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        public CreateRequestCommandHandler(IUnitOfWorkRepository repo, IMapper mapper, INotificationService notificationService)
        {
            _repo = repo;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<Result> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {

            RequestDTO? resultObject = null;
            Guid roomId = new Guid(request.RoomId);
            var account= await _repo.accountRepo.GetByAccountId(request.AccountId);
            var listfacilityHeads = await _repo.accountRepo.GetAllFacilityHeads();
            var problemId = Convert.ToInt32(request.ProblemId);

            var problemdData = await _repo.problemRepo.GetProblemById(problemId);
            if (problemdData == null)
                return Result.Failure(new Error("Error.Client", "Data does not exist"), "ProblemId does not exist.");

            //check duplicate request on a room(spam request)
            if (account != null) {
                var existedUnprocessedRequestOnRoom = account.Requests!.
                                 Where(r => r.RoomId == roomId)
                                .Where(r=> r.RequestStatusId != 5 && r.RequestStatusId != 6 && r.RequestStatusId !=7)
                                .ToArray();
                if (existedUnprocessedRequestOnRoom.Length > 3)
                {
                    Error error = new("Error.RequestCommandHandler", "You have a unprocessed requet on this room. Please wait!");
                    return Result.Failure(error, "Create request failed!");
                }
            }

            var requestData = new Request
            {
                Id = new Guid(),
                AccountId = request.AccountId,
                RoomId = roomId,
                RequestStatusId = 1,
                ProblemId = problemId,
                Description = request.Description,
                SeveralLevel = request.SeveralLevel,
                Reason = "",
                Date= request.Date != null ? request.Date : null,
                Enable = true,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
            _repo.requestRepo.Add(requestData);
            resultObject = _mapper.Map<RequestDTO>(requestData);


            //creat notification for facility-heads
            List<NotificationQueue> notificationQueues = new List<NotificationQueue>();
            foreach (var item in listfacilityHeads)
            {
                var notificationCreate = new NotificationQueue
                {
                    Id = new Guid(),
                    NotificationTypeId = 1,
                    AccountId = item.AccountId,
                    AccountSenderId = requestData.AccountId,
                    RequestId = requestData.Id,
                    NotificationTitle = "<span className='font-semibold text-gray-900 dark:text-white'>" + account.FullName + "</span>" +
                                    " created a new request about the problem " +
                                    "<span className='font-semibold text-gray-900 dark:text-white'>" + problemdData.Title + "</span>",
                    IsViewed = false,
                    NotificationTime = DateTime.Now
                };
                notificationQueues.Add(notificationCreate);
                _repo.notificationQueueRepo.Add(notificationCreate);

                var notificationRemark = new Domain.Entities.Requests.NotificationRemark
                {
                    Id = new Guid(),
                    RequestId = requestData.Id,
                    AccountId = item.AccountId,
                    IsSeen = true,
                    Unwatchs = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _repo.notificationRemarkRepo.Add(notificationRemark);
            }

            var notificationRe = new Domain.Entities.Requests.NotificationRemark
            {
                Id = new Guid(),
                RequestId = requestData.Id,
                AccountId = request.AccountId,
                IsSeen = true,
                Unwatchs = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _repo.notificationRemarkRepo.Add(notificationRe);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);
                foreach (var item in notificationQueues)
                {
                    var notifications = await _repo.notificationQueueRepo.GetNotificationCreatedRequest(item.Id, item.AccountId);
                    var resultNotice = _mapper.Map<NotificationQueueDTO>(notifications);
                    await _notificationService.RequestCreateNotification(item.AccountId, resultNotice);
                }
                return Result.Success(resultObject, "Create request successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error error = new("Error.RequestCommandHandler", "There is an error saving data!");
                return Result.Failure(error, "Create request failed!");
            }
        }
    }
}
