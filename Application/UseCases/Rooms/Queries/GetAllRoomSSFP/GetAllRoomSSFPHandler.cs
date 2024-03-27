using System;
using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Departments;
using Application.UseCases.Departments.Queries.GetListDepartmentSSFP;
using AutoMapper;
using Domain.Entities.Departments;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Rooms.Queries.GetAllRoomSSFP
{
    public sealed class GetAllRoomSSFPHandler
        : IQueryHandler<GetAllRoomSSFPQueries, PagedList<RoomDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAllRoomSSFPHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Result<PagedList<RoomDTO>>> Handle(GetAllRoomSSFPQueries request, CancellationToken cancellationToken)
        {
            var list = await _repo.roomRepo.GetListRoomSSFP
                (request.SearchTerm,request.SortDepartmentName ,request.Page, request.Limit, cancellationToken);
            if (list == null)
            {
                return Result.Failure<PagedList<RoomDTO>>(new Error("Error.Empty", "data null"), "List Rooms SSFP is Null");
            }
            var resultList = _mapper.Map<List<RoomDTO>>(list.Items);
            var resultPageList = new PagedList<RoomDTO>
            {
                Items = resultList,
                Page = request.Page,
                Limit = request.Limit,
                TotalCount = list.TotalCount
            };

            return Result.Success<PagedList<RoomDTO>>(resultPageList, "Get List Rooms SSFP successfully !");
        }
    }
}

