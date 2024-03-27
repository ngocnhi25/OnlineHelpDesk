using Application.Common.Mapppings;
using Application.DTOs.Accounts;
using Application.DTOs.Requests;
using Domain.Entities.Accounts;
using Domain.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Remarks
{
    public class RemarkDTO : IMapForm<Remark>
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string AccountId { get; set; }
        public string Comment { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Enable { get; set; }
        public AccountResponse? Account { get; set; }
        public RequestDTO? Request { get; set; }
    }
}
