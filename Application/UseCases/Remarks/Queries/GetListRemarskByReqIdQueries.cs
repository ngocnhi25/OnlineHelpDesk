using Application.Common.Messaging;
using Application.DTOs.Remarks;

namespace Application.UseCases.Remarks.Queries
{
    public class GetListRemarskByReqIdQueries : IQuery<List<RemarkDTO>>
    {
        public string? RequestId { get; set; }
    }
}
