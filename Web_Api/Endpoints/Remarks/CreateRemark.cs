using Application.UseCases.Remarks.Command.CreateRemark;
using Ardalis.ApiEndpoints;
using Domain.Repositories;
using Infrastructure.sHubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
    public class CreateRemark : EndpointBaseAsync
     .WithRequest<CreateRemarkCommand>
     .WithActionResult<Result>
    {

        private readonly IMediator Sender;
        private readonly IUnitOfWorkRepository _repo;
        public readonly IHubContext<ChatHub> _hubContext;
        public readonly IHubContext<NotificationHub> _hubNotifiContext;
        public CreateRemark(IMediator sender, IUnitOfWorkRepository repo, IHubContext<ChatHub> hubContext, IHubContext<NotificationHub> hubNotifiContext)
        {
            Sender = sender;
            _repo = repo;
            _hubContext = hubContext;
            _hubNotifiContext = hubNotifiContext;
        }


        [HttpPost("api/request/create_remark")]
        public override async Task<ActionResult<Result>> HandleAsync(CreateRemarkCommand command, CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);

            if(status != null && status.IsSuccess == true)
            {
                var roomId = status.Data.Request!.Id.ToString(); // dua tren requestId làm room vì nó unique
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(status.Data, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
                });
                await _hubContext.Clients.Group(roomId).SendAsync("ReceiveMessage", jsonString);

            }

            if (status != null && status.IsSuccess == true)
            {
                var roomId = status.Data.Request!.Id.ToString(); // dua tren requestId làm room vì nó unique
                var notifiRemark = await _repo.notificationRemarkRepo.GetNotificationRemarkByRequestId(status.Data.Request!.Id);
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(notifiRemark, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
                });
                await _hubNotifiContext.Clients.Group(roomId).SendAsync("ReceiveNotificationRemark", jsonString);
            }

            return status!;
        }
    }
}