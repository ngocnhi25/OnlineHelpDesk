namespace Application.Services
{
    public interface INotificationService
    {
        Task RequestCreateNotification(string accountId, object json);
    }
}
