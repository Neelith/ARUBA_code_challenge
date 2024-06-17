using Application.Commons.Constants;
using Domain.Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FileReaderService
{
    public class FileReaderService : IFileReaderService
    {
        public async Task<byte[]> ReadFileContentAsync(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new BadRequestException(ApplicationErrors.FileIsNotValid);
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
