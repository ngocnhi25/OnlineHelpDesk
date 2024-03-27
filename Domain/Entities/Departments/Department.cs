namespace Domain.Entities.Departments
{
    public class Department
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; }
        public bool StatusDepartment { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
