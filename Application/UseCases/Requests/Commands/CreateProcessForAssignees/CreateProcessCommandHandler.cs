using Application.Common.Messaging;
using Application.DTOs.Notificarions;
using Application.Services;
using AutoMapper;
using Domain.Entities.Notifications;
using Domain.Entities.Requests;
using Domain.Repositories;
using SharedKernel;


namespace Application.UseCases.Requests.Commands.CreateProcessForAssignees
{
    public sealed class CreateProcessCommandHandler : ICommandHandler<CreateProcessCommand>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public CreateProcessCommandHandler(IUnitOfWorkRepository repo, INotificationService notificationService, IMapper mapper)
        {
            _repo = repo;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<Result> Handle(
            CreateProcessCommand request,
            CancellationToken cancellationToken)
        {
            Guid requestId = new Guid(request.RequestId);
            var requestAlreadyHanded = await _repo.assigneesRepo.GetRequestById(requestId);
            if(requestAlreadyHanded != null)
            {
                return Result.Failure(new Error("Error.CreateProcessHandler", "REquestId is exsit!"),
                   "Request is exsit!");
            }

            var account = await _repo.accountRepo.GetByAccountId(request.AccountId);
            if (account == null )
            {
                return Result.Failure(new Error("Error.CreateProcessHandler", "Data is null!"),
                    "Account does not exsit!");
            }
            if(account.RoleId != 4)
            {
                return Result.Failure(new Error("Error.CreateProcessHandler", "RoleID is null!"),
                    "Assignees does not exsit !");
            }
            if(account.StatusAccount != "Active")
            {
                return Result.Failure(new Error("Error.CreateProcessHandler", "StatusAccount is error!"),
                    "Assignees Status is InActive or Banned  !");
            }

            var requestItem = await _repo.requestRepo.GetRequestById(requestId);
            if (requestItem == null)
            {
                return Result.Failure(new Error("Error.CreateProcessHandler", "Something is null!"),
                   "Request does not exsit !");
            }

            if (requestItem.RequestStatusId != 1)
            {
                return Result.Failure(new Error("Error.CreateProcessHandler", "Status Name is error!"),
                  "Status Name is error !");
            }

            var processByAssignee = await _repo.assigneesRepo
                .GetByAssigneeHandleRequest(request.AccountId, requestId);
            if (processByAssignee != null)
            {
                return Result.Failure(new Error("Error.CreateProcessHandler", "Something is null!"),
                   "RequestId or AccountId is duplicate !");
            }

            requestItem.RequestStatusId = 2 ;
            requestItem.UpdateAt = DateTime.Now;
            _repo.requestRepo.Update(requestItem);

            var processByAssigneesData = new ProcessByAssignees
            {
                RequestId = requestId,
                AccountId = request.AccountId,
            };
            _repo.assigneesRepo.Add(processByAssigneesData);

            List<NotificationQueue> notificationQueues = new List<NotificationQueue>();
            var notificationCreateForAssignee = new NotificationQueue
            {
                Id = new Guid(),
                NotificationTypeId = 2,
                AccountId = request.AccountId,
                AccountSenderId = request.FacilityHeadId,
                RequestId = requestId,
                NotificationTitle = "<span className='font-semibold text-gray-900 dark:text-white'>" + request.FullNameFacilityHeads + "</span>" +
                                    " has assigned you to handle the request for issue " +
                                    "<span className='font-semibold text-gray-900 dark:text-white'>" + requestItem.Problem!.Title + "</span>",
                IsViewed = false,
                NotificationTime = DateTime.Now
            };
            notificationQueues.Add(notificationCreateForAssignee);
            _repo.notificationQueueRepo.Add(notificationCreateForAssignee);

            var notificationCreateForRequestor = new NotificationQueue
            {
                Id = new Guid(),
                NotificationTypeId = 2,
                AccountId = requestItem.Account!.AccountId,
                AccountSenderId = request.FacilityHeadId,
                RequestId = requestId,
                NotificationTitle = "<span className='font-semibold text-gray-900 dark:text-white'>" + request.FullNameFacilityHeads + "</span>" +
                                    " has assigned " +
                                    "<span className='font-semibold text-gray-900 dark:text-white'>" + account.FullName + "</span>" +
                                    " to handle your request for the problem " +
                                    "<span className='font-semibold text-gray-900 dark:text-white'>" + requestItem.Problem!.Title + "</span>",
                IsViewed = false,
                NotificationTime = DateTime.Now
            };
            notificationQueues.Add(notificationCreateForRequestor);
            _repo.notificationQueueRepo.Add(notificationCreateForRequestor);

            var notificationRemark = new NotificationRemark
            {
                Id = new Guid(),
                RequestId = requestId,
                AccountId = request.AccountId,
                IsSeen = true,
                Unwatchs = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _repo.notificationRemarkRepo.Add(notificationRemark);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);
                foreach (var item in notificationQueues)
                {
                    var notifications = await _repo.notificationQueueRepo.GetNotificationCreatedRequest(item.Id, item.AccountId);
                    var resultNotice = _mapper.Map<NotificationQueueDTO>(notifications);
                    await _notificationService.RequestCreateNotification(item.AccountId, resultNotice);
                }
                return Result.Success("Created request processByAssigneesData successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error error = new("Error.CreateProcessHandler", "There is an error saving data!");
                return Result.Failure(error, "Create request processByAssigneesData FAILED !");
            }
        }


    }
}

