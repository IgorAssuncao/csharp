using Microsoft.AspNetCore.Http;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ImageService : IImageService
    {
        private IImageRepository ImageRepository { get; set; }

        public ImageService(IImageRepository imageRepository)
        {
            ImageRepository = imageRepository;
        }

        public async Task<string> UploadImage(Guid id, IFormFile file)
        {
            string extension = file.FileName.Split('.')[1];
            StreamReader stream = new StreamReader(file.OpenReadStream());
            return await ImageRepository.UploadFile(id.ToString(), extension, stream.BaseStream);
        }
    }
}
