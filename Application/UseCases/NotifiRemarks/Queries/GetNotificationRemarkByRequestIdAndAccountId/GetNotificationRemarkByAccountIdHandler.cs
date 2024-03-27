using Application.Common.Messaging;
using Application.DTOs.Requests;
using Application.UseCases.Requests.Queries.GetAllRequestWithoutSSFP;
using AutoMapper;
using Domain.Entities.Requests;
using Domain.Repositories;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.NotifiRemark.Queries.GetNotificationRemarkByRequestIdAndAccountId
{
    public class GetNotificationRemarkByAccountIdHandler : IQueryHandler<GetNotificationRemarkByAccountIdQueries, List<NotificationRemark>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetNotificationRemarkByAccountIdHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<NotificationRemark>>> Handle(GetNotificationRemarkByAccountIdQueries request, CancellationToken cancellationToken)
        {

            var list = await _repo.notificationRemarkRepo.GetNotificationRemarkByAccountId(request.AccountId);
            if (list == null)
            {
                return Result.Failure<List<NotificationRemark>>(new Error("Error.Empty", "data null"), "List Request is Empty");
            }
            var resultList = _mapper.Map<List<NotificationRemark>>(list);


            return Result.Success<List<NotificationRemark>>(resultList, "Get List Request successfully !");
        }
    }
}
