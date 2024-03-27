using Application.Common.Messaging;
using Application.DTOs.Remarks;
using Application.DTOs.Requests;
using Application.UseCases.Remarks.Queries.GetListRemarskByAccountId;
using AutoMapper;
using Domain.Entities.Requests;
using Domain.Repositories;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Remarks.Queries
{
    public class GetListRemarskByAccountIdHandler : IQueryHandler<GetListRemarskByAccountIdQueries, List<RemarkDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetListRemarskByAccountIdHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Result<List<RemarkDTO>>> Handle(GetListRemarskByAccountIdQueries request, CancellationToken cancellationToken)
        {
            string accountId = request.AccountId;
            var list = await _repo.remarkRepo.GetRemarksByAccountId(accountId);
            if (list == null)
            {
                return Result.Failure<List<RemarkDTO>>
                    (new Error("Error.Empty", "data null"), "List Remark Data is Null");
            }

            var resultList = _mapper.Map<List<RemarkDTO>>(list);

            return Result.Success<List<RemarkDTO>>
                (resultList, "Get List Remark data successfully !");

        }
    }
}
