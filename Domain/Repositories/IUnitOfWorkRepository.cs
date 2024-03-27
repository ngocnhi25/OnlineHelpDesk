namespace Domain.Repositories
{
    public interface IUnitOfWorkRepository
    {

        IAccountRepository accountRepo { get; }
        IRequestRepository requestRepo { get; }
        IRoleRepository roleRepo { get; }
        IRoleTypeRepository roleTypeRepo { get; }
        IDepartmentRepository departmentRepo { get; }
        IRoomRepository roomRepo { get; }
        IAssigneesRepository assigneesRepo { get; }
        IRemarkRepository remarkRepo { get; }
        IRequestStatusRepository requestStatusRepo { get; }
        INotificationRemarkRepository notificationRemarkRepo {  get; }
        INotificationHandleRequestRepository notificationHandleRequestRepo { get; }
        INotificationTypeRepository notificationTypeRepo { get; }
        INotificationQueueRepository notificationQueueRepo { get; }
        IProblemRepository problemRepo { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
