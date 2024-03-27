using Application;
using Domain.Entities.Settings;
using Infrastructure;
using Infrastructure.sHubs;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));

builder.Services
    .AddApplicationService()
    .AddInfrastructureService(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSignalR();
var allowOrigins = builder.Configuration.GetSection("AllowOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.WithOrigins(allowOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("myAppCors");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");
app.MapHub<NotificationHub>("/hubs/notificationRemark");
app.MapHub<BannedHub>("/hubs/bannedAccount");
app.MapHub<NotificationHub>("/hubs/notificationForAccount");
app.Run();
