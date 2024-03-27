using Application.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Domain.Entities.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly AsuzeOptions _asuzeOptions;
        public FileService(IOptions<AsuzeOptions> asuzeOptions)
        {
            _asuzeOptions = asuzeOptions.Value;
        }

        public async Task<Tuple<int, string>> SaveImage(IFormFile imageFile)
        {
            BlobContainerClient blobContainerClient = new BlobContainerClient(
                    _asuzeOptions.ConnectionString,
                    _asuzeOptions.Container);

            using MemoryStream fileUploadStream = new MemoryStream();
            try
            {
                var ext = Path.GetExtension(imageFile.FileName);
                imageFile.CopyTo(fileUploadStream);
                fileUploadStream.Position = 0;

                string fileType;
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (ext.EndsWith(".png"))
                {
                    fileType = "image/png";
                }
                else if (ext.EndsWith(".jpg"))
                {
                    fileType = "image/jpg";
                }
                else if (ext.EndsWith(".jpeg"))
                {
                    fileType = "image/jpeg";
                }
                else
                {
                    string msg = string.Format("Only {0} extension aer allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }

                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;

                BlobClient blobClient = blobContainerClient.GetBlobClient(newFileName);

                await blobClient.UploadAsync(fileUploadStream, new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = fileType
                    }
                }, cancellationToken: default);
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Tuple<int, string>(0, "Error has occured");
            }
        }

        public async Task DeleteImage(string? imageFileName)
        {
            if(imageFileName != null)
            {
                BlobContainerClient blobContainerClient = new BlobContainerClient(
                            _asuzeOptions.ConnectionString,
                            _asuzeOptions.Container);
                BlobClient blobClient = blobContainerClient.GetBlobClient(imageFileName);
                await blobClient.DeleteAsync();
            }
        }
    }
}
