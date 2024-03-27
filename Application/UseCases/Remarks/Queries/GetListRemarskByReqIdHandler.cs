using Application.Common.Messaging;
using Application.DTOs.Remarks;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Remarks.Queries
{
    public class GetListRemarskByReqIdHandler : IQueryHandler<GetListRemarskByReqIdQueries, List<RemarkDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetListRemarskByReqIdHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Result<List<RemarkDTO>>> Handle(GetListRemarskByReqIdQueries request, CancellationToken cancellationToken)
        {
            Guid requestId = new Guid(request.RequestId);
            var list = await _repo.remarkRepo.GetRemarksRequestId(requestId);
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
