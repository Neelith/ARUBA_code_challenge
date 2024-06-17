using Microsoft.AspNetCore.Http;

namespace Application.Services.FileReaderService
{
    public interface IFileReaderService
    {
        Task<byte[]> ReadFileContentAsync(IFormFile file);
    }
}
