using Application.Common.Messaging;
using Application.DTOs.Notificarions;
using Application.Services;
using AutoMapper;
using Domain.Entities.Notifications;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Requests.Commands.ProcessUpdateStatusRequest
{
    public sealed class ProcessUpdateStatusRequestCommandHandler : ICommandHandler<ProcessUpdateStatusRequestCommand>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public ProcessUpdateStatusRequestCommandHandler(IUnitOfWorkRepository repo, IMapper mapper, INotificationService notificationService)
        {
            _repo = repo;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<Result> Handle(ProcessUpdateStatusRequestCommand request, CancellationToken cancellationToken)
        {
            Guid requestId = new Guid(request.Id);
            int requestStatusId = int.Parse(request.RequestStatusId);
            var requestStatus = await _repo.requestStatusRepo.GetRequestStatusById(requestStatusId);
            if (requestStatus == null)
                return Result.Failure(new Error("Error", "No data exists"), "RequestStatus does not exits");

            var resultRequest = await _repo.requestRepo.GetRequestById(requestId);
            if (resultRequest == null)
                return Result.Failure(new Error("Error", "No data exists"), "The request does not exist in the database");

            if (resultRequest.RequestStatus!.Id == 6)
                return Result.Failure(new Error("Error", "wrong state"), "Complete processing of the user's request. Can't change the status of a request");

            if (resultRequest.RequestStatus!.Id == 5)
                return Result.Failure(new Error("Error", "wrong state"), "The request is in a rejected state. The status of the request can't be changed");

            if (
                requestStatusId != 3 
                && requestStatusId != 4
                && requestStatusId != 5
                && requestStatusId != 6
            ) return Result.Failure(new Error("Error", "wrong state"), "You can only update Work in progress, Need more info, Rejected and Completed states");

            if(requestStatusId <= resultRequest.RequestStatus!.Id)
                return Result.Failure(new Error("Error", "wrong state"), "Can't go back to the old state");

            if(requestStatusId == 5 && request.Reason != null)
            {
                resultRequest.Reason = request.Reason;
            }

            resultRequest.RequestStatusId = requestStatusId;
            resultRequest.UpdateAt = DateTime.Now;
            _repo.requestRepo.Update(resultRequest);

            var listfacilityHeads = await _repo.accountRepo.GetAllFacilityHeads();
            List<NotificationQueue> notificationQueues = new List<NotificationQueue>();
            foreach (var item in listfacilityHeads)
            {
                var notificationForFacilityHeads = new NotificationQueue
                {
                    Id = new Guid(),
                    NotificationTypeId = 3,
                    AccountId = item.AccountId,
                    AccountSenderId = request.AccountIdSender,
                    RequestId = requestId,
                    NotificationTitle = "<span className='font-semibold text-gray-900 dark:text-white'>" + request.FullNameAccountSender + "</span>" +
                                    " updates the status of " +
                                    "<span className='font-semibold text-gray-900 dark:text-white'>" + resultRequest.Account!.FullName + "</span>" +
                                    " request on the issue " +
                                    "<span className='font-semibold text-gray-900 dark:text-white'>" + resultRequest.Problem!.Title + "</span>" +
                                    " to " + "<span className='font-semibold text-gray-900 dark:text-white'>" + requestStatus.StatusName + "</span>",
                    IsViewed = false,
                    NotificationTime = DateTime.Now
                };
                notificationQueues.Add(notificationForFacilityHeads);
                _repo.notificationQueueRepo.Add(notificationForFacilityHeads);
            }

            var notificationForRequestor = new NotificationQueue
            {
                Id = new Guid(),
                NotificationTypeId = 3,
                AccountId = resultRequest.AccountId,
                AccountSenderId = request.AccountIdSender,
                RequestId = requestId,
                NotificationTitle = "<span className='font-semibold text-gray-900 dark:text-white'>" + request.FullNameAccountSender + "</span>" + 
                                    " updates the status of your request on issue " +
                                    "<span className='font-semibold text-gray-900 dark:text-white'>" + resultRequest.Problem!.Title + "</span>" +
                                    " to " + "<span className='font-semibold text-gray-900 dark:text-white'>" + requestStatus.StatusName + "</span>",
                IsViewed = false,
                NotificationTime = DateTime.Now
            };
            notificationQueues.Add(notificationForRequestor);
            _repo.notificationQueueRepo.Add(notificationForRequestor);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);
                foreach (var item in notificationQueues)
                {
                    var notifications = await _repo.notificationQueueRepo.GetNotificationCreatedRequest(item.Id, item.AccountId);
                    var resultNotice = _mapper.Map<NotificationQueueDTO>(notifications);
                    await _notificationService.RequestCreateNotification(item.AccountId, resultNotice);
                }
                return Result.Success("Update status of requestId " + request.Id + " successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error error = new("Error.UpdateRequest", "There is an error saving data!");
                return Result.Failure(error, "Account code verification errors");
            }
        }
    }
}
