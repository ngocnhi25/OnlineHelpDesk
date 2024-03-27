using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Departments;
using Domain.Entities.Requests;
using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OHDDbContext : DbContext
    {
        public OHDDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<ProcessByAssignees> ProcessByAssignees { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Remark> Remarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OHDDbContext).Assembly);
        }
    }
}
