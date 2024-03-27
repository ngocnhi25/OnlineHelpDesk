using Domain.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.sHubs
{
    public class NotificationHub : Hub
    {
        public async Task NotificationForAccount(string accountId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, accountId);
            /*await Clients.Group(accountId).SendAsync("NotificationForAccount", notificationName);*/
        }
    }
}
