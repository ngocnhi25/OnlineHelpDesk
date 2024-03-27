using Application.Common.Mapppings;
using Domain.Entities.Departments;

namespace Application.DTOs.Departments
{
    public class RoomDTO : IMapForm<Room>
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public bool RoomStatus { get; set; }
        public DepartmentDTOWithoutRoomList? Departments { get; set; }
    }
}
