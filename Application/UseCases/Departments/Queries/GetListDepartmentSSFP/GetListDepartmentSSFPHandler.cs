using System;
using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;
using Application.UseCases.Requests.Queries.GetAllRequest;
using AutoMapper;
using Domain.Entities.Departments;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Departments.Queries.GetListDepartmentSSFP
{
    public sealed class GetListDepartmentSSFPHandler
        : IQueryHandler<GetListDepartmentSSFPQueries, PagedList<Department>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetListDepartmentSSFPHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<Department>>> Handle
            (GetListDepartmentSSFPQueries request, CancellationToken cancellationToken)
        {
            var list = await _repo.departmentRepo.GetListDepartmentSSFP
                (request.SearchTerm, request.Page, request.Limit, cancellationToken);
            if (list == null)
            {
                return Result.Failure<PagedList<Department>>(new Error("Error.Empty", "data null"), "List Department SSFP is Null");
            }
            var resultList = _mapper.Map<List<Department>>(list.Items);
            var resultPageList = new PagedList<Department>
            {
                Items = resultList,
                Page = request.Page,
                Limit = request.Limit,
                TotalCount = list.TotalCount
            };

            return Result.Success<PagedList<Department>>(resultPageList, "Get List Department SSFP successfully !");
        }
    }
}

