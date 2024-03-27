using Domain.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public sealed class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly OHDDbContext _dbContext;
        public IAccountRepository accountRepo {  get; private set; }

        public IRequestRepository requestRepo { get; private set; }

        public IRoleRepository roleRepo {  get; private set; }

        public IRoleTypeRepository roleTypeRepo {  get; private set; }

        public IDepartmentRepository departmentRepo { get; private set; }

        public IRoomRepository roomRepo { get; private set; }

        public IAssigneesRepository assigneesRepo { get; private set; }

        public IRemarkRepository remarkRepo { get; private set; }

        public IRequestStatusRepository requestStatusRepo { get; private set; }

        public INotificationRemarkRepository notificationRemarkRepo {  get; private set; }
        public INotificationHandleRequestRepository notificationHandleRequestRepo{ get; private set; }

        public INotificationTypeRepository notificationTypeRepo { get; private set; }

        public INotificationQueueRepository notificationQueueRepo { get; private set; }

        public IProblemRepository problemRepo { get; private set; }

        public UnitOfWorkRepository(OHDDbContext dbContext)
        {
            _dbContext = dbContext;
            accountRepo = new AccountRepository(dbContext);
            requestRepo = new RequestRepository(dbContext);
            roleRepo = new RoleRepository(dbContext);
            roleTypeRepo = new RoleTypeRepository(dbContext);
            departmentRepo = new DepartmentRepository(dbContext);
            roomRepo = new RoomRepository(dbContext);
            assigneesRepo = new AssigneesRepository(dbContext);
            remarkRepo = new RemarkRepository(dbContext);
            requestStatusRepo = new RequestStatusRepositoty(dbContext);
            notificationRemarkRepo = new NotificationRemarkRepository(dbContext);
            notificationHandleRequestRepo = new NotificationHandleRequestRepository(dbContext);
            notificationTypeRepo = new NotificationTypeRepository(dbContext);
            notificationQueueRepo = new NotificationQueueRepository(dbContext);
            problemRepo = new ProblemRepository(dbContext);
        }


        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error rolling back transaction: {ex.Message}");
            }
        }
    }
}
