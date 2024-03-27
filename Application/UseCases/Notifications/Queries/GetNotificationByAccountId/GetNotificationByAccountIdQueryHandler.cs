using Application.Common.Messaging;
using Application.DTOs.Notificarions;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Notifications.Queries.GetNotificationByAccountId
{
    public sealed class GetNotificationByAccountIdQueryHandler : IQueryHandler<GetNotificationByAccountIdQuery, List<NotificationQueueDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetNotificationByAccountIdQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<NotificationQueueDTO>>> Handle(GetNotificationByAccountIdQuery request, CancellationToken cancellationToken)
        {
            if(request.AccountId == null)
            {
               return Result.Failure<List<NotificationQueueDTO>>(new Error("Error.Empty", "data null"), "List Request is Null");
            }

            var listNoti = await _repo.notificationQueueRepo.GetNotificationByAccountId(request.SortIsViewed, request.Page, request.AccountId, cancellationToken);
            var resultObject = _mapper.Map<List<NotificationQueueDTO>>(listNoti);
            return resultObject;
        }
    }
}
