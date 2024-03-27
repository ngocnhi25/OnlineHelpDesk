using Application.Common.Messaging;
using Application.DTOs.Requests;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Requests.Queries.GetAllRequestWithoutSSFP
{
    public sealed record GetAllRequestWithoutSSFPQuery(string? AccountId) :
        IQuery<List<RequestDTO>>;
}
