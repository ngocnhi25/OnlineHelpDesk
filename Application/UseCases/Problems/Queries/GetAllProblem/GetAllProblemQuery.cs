using Application.Common.Messaging;
using Application.DTOs.Requests;

namespace Application.UseCases.Problems.Queries.GetAllProblem
{
    public sealed record GetAllProblemQuery() : IQuery<List<ProblemDTO>>;
}
