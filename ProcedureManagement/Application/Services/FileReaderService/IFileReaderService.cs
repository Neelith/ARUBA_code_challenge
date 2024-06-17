using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FileReaderService
{
    public interface IFileReaderService
    {
        Task<byte[]> ReadFileContentAsync(IFormFile file);
    }
}
