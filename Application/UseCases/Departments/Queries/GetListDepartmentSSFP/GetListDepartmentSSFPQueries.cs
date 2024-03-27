using Application.Common.Messaging;
using Application.DTOs;
using Domain.Entities.Departments;

namespace Application.UseCases.Departments.Queries.GetListDepartmentSSFP
{
    public sealed record GetListDepartmentSSFPQueries
         (string? SearchTerm, int Page, int Limit)
        : IQuery<PagedList<Department>>;

}

