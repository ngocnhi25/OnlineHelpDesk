using Application.Common.Mapppings;
using Domain.Entities.Requests;

namespace Application.DTOs.Requests
{
    public class ProblemDTO : IMapForm<Problem>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDisplay { get; set; }
    }
}
