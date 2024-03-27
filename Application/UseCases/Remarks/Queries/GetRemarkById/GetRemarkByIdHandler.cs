using Application.Common.Messaging;
using Application.DTOs.Remarks;
using AutoMapper;
using Domain.Entities.Requests;
using Domain.Repositories;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Remarks.Queries.GetRemarkById
{
    public class GetRemarkByIdHandler : IQueryHandler<GetRemarkByIdQueries, Remark>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;
        public GetRemarkByIdHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<Remark>> Handle(GetRemarkByIdQueries request, CancellationToken cancellationToken)
        {
            //Guid remarkId = new Guid(request.RemarkId);
            var remark = await _repo.remarkRepo.GetLatestRemark(request.RemarkId);
            if (remark == null)
            {
                return Result.Failure<Remark>
                    (new Error("Error.Empty", "data null"), "List Remark Data is Null");
            }

            return Result.Success<Remark>
                (remark, "Get List Remark data successfully !");

        }
    }
}
