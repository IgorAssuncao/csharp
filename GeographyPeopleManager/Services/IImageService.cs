using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Services
{
    public interface IImageService
    {
        Task<string> UploadImage(Guid id, IFormFile file);
    }
}