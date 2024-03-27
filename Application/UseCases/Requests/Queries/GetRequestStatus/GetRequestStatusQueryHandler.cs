using System;
using System.Collections.Generic;
using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;
using Application.UseCases.Requests.Queries.GetAllClientRequest;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Requests.Queries.GetRequestStatus
{
    public sealed class GetRequestStatusQueryHandler :
        IQueryHandler<GetRequestStatusQuery, IEnumerable <RequestStatusDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetRequestStatusQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RequestStatusDTO>>> Handle(GetRequestStatusQuery request, CancellationToken cancellationToken)
        {
            var requestQueryObj = await _repo.requestStatusRepo.GetAll();
            if (requestQueryObj == null)
            {
                return Result.Failure<IEnumerable<RequestStatusDTO>> (new Error("Error.Empty", "data null"), "Request Status is Null");
            }

            var resultObject = _mapper.Map< IEnumerable<RequestStatusDTO>>(requestQueryObj);
            return Result.Success<IEnumerable<RequestStatusDTO>>(resultObject, "Get RequestStatus successfully !");
        }
    }
}

