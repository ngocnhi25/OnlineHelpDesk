using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public interface IFileService
    {
        Task<Tuple<int, string>> SaveImage(IFormFile imageFile);

        Task DeleteImage(string? imageFileName);
    }
}
