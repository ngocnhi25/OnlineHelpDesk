using Application.Common.Mapppings;
using Application.Services;
using Domain.Repositories;
using Infrastructure.sHubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        public readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task RequestCreateNotification(string accountId, object json)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(json, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
            });
            await _hubContext.Clients.Group(accountId).SendAsync("NotificationForAccount", jsonString);
        }
    }
}
