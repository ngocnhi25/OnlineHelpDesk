using Application.Common.Mapppings;
using Domain.Entities.Departments;

namespace Application.DTOs.Departments
{
    public class DepartmentDTOWithoutRoomList : IMapForm<Department>
    {
        public string DepartmentName { get; set; }
    }
}
