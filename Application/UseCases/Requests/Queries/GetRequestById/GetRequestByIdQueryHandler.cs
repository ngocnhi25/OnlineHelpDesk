using Application.Common.Messaging;
using Application.DTOs.Requests;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Requests.Queries.GetRequestById
{
    public sealed class GetRequestByIdQueryHandler : IQueryHandler<GetRequestByIdQuery, RequestDTO>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetRequestByIdQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<RequestDTO>> Handle
            (GetRequestByIdQuery request, CancellationToken cancellationToken)
        {
            Guid id = request.Id;
            var requestQueryObj = await _repo.requestRepo.GetRequestById(id);
            if(requestQueryObj == null)
            {
                return Result.Failure<RequestDTO>(new Error("Error.Empty", "data null"), "Request is Null");
            } 

            var resultObject = _mapper.Map<RequestDTO>(requestQueryObj);
            return  Result.Success<RequestDTO>(resultObject, "Get Request successfully !");
        }
    }
}
