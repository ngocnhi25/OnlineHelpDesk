using Application.DTOs.Auth;

namespace Application.Services
{
    public interface IMailService
    {
        Task SendMailAsync(MailRequest mailRequest);
    }
}
