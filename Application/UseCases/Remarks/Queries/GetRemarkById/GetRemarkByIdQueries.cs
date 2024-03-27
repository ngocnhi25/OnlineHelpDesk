using Application.Common.Messaging;
using Application.DTOs.Remarks;
using Domain.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Remarks.Queries.GetRemarkById
{
    public class GetRemarkByIdQueries : IQuery<Remark>
    {
        public string? RemarkId { get; set; }
    }
}
