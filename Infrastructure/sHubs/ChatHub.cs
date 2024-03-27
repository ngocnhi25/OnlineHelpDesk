using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Infrastructure.sHubs
{
    public class ChatHub : Hub
    {
        private readonly IUnitOfWorkRepository _repo;

        public ChatHub(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task SendMessage(string accountId ,string username, string message)
        { 
           await Clients.All.SendAsync("ReceiveMessage", username, message);
        }

        public async Task ReceiveNotificationRemark()
        {
            var list = _repo.remarkRepo.GetRemarksRequestId(Guid.Parse("ds"));
            await Groups.AddToGroupAsync(Context.ConnectionId, "");
 
        }

        public async Task JoinSpecificChatRoom(string requestId, string userName) // tao phong dua tren requestId 
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, requestId);
            await Clients.Group(requestId).SendAsync("JoinSpecificChatRoom", userName, $"{userName} has joined");
        }

        public async Task LeaveSpecificChatRoom(string requestId, string userName) // roi phong dua tren requestId 
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, requestId);
            await Clients.Group(requestId).SendAsync("LeaveSpecificChatRoom", userName, $"{userName} has left");
        }

        
    }
}
