using Application.Common.Mapppings;
using Domain.Entities.Departments;

namespace Application.DTOs.Departments
{
    public class DepartmentDTO : IMapForm<Department>
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; }
        public bool StatusDepartment { get; set; }
        public List<RoomDTO>? Rooms { get; set; }
    }
}
