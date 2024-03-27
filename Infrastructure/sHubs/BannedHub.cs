using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.sHubs
{
    public class BannedHub : Hub
    {
        public async Task LogoutAccountWhenBanned(string accountId) 
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, accountId);
        }

    }
}
