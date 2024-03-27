using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;
using Application.UseCases.Requests.Queries.GetAllRequestWithoutSSFP;
namespace Application.UseCases.Requests.Queries.GetAllRequestWithoutSSFP
{
    public sealed class GetAllRequestWithoutSSFPQueryHandler
        : IQueryHandler<GetAllRequestWithoutSSFPQuery, List<RequestDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAllRequestWithoutSSFPQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<RequestDTO>>> Handle(GetAllRequestWithoutSSFPQuery request, CancellationToken cancellationToken)
        {

            var list = await _repo.requestRepo.GetAllRequestWithoutSSFP(request.AccountId);
            if (list == null)
            {
                return Result.Failure<List<RequestDTO>>(new Error("Error.Empty", "data null"), "List Request is Empty");
            }
            var resultList = _mapper.Map<List<RequestDTO>>(list);
          

            return Result.Success<List<RequestDTO>>(resultList, "Get List Request successfully !");
        }
    }
}
