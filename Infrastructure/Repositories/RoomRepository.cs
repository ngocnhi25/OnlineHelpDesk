using Domain.Entities.Departments;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class RoomRepository: GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(OHDDbContext dbContext)
                   : base(dbContext) { }

        public async Task<DataResponse<Room>> GetListRoomSSFP(string? searchTerm, string? sortDepartmentName, int page, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<Room> roomQuery = _dbContext.Set<Room>()
                .Include(a => a.Departments);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roomQuery = roomQuery.Where(a =>
                a.RoomNumber.Contains(searchTerm)
                );
            }

            if (!string.IsNullOrEmpty(sortDepartmentName))
            {
                roomQuery = roomQuery
                    .Where(a => a.Departments!.DepartmentName == sortDepartmentName);
            };



            var totalCount = await roomQuery.CountAsync();

            var rooms = await roomQuery
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            return new DataResponse<Room>
            {
                Items = rooms, 
                TotalCount = totalCount,
            };
        }


        public Task<Room?> GetRoomById(Guid id)
        {
            var room = _dbContext.Set<Room>().SingleOrDefaultAsync(d => d.Id == id);
            return room;
        }

        public Task<Room?> GetRoomByRoomNumber(string roomNumber)
        {
            var room = _dbContext.Set<Room>().SingleOrDefaultAsync(d => d.RoomNumber == roomNumber);
            return room;
        }
    }
}
