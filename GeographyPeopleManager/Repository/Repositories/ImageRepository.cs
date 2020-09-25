using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private BlobContainerClient ContainerClient { get; set; }
        private BlobClient BlobClient { get; set; }

        public ImageRepository()
        {
            ContainerClient = new BlobContainerClient(@"DefaultEndpointsProtocol=https;AccountName=atvitor;AccountKey=ETfOj9/FTeDctV4M53EHQljH2UCBkQQ7crsiJ5M1LnCgZhmKKxdTvb1v8o7kc/nHJCgWST/WrAwvm+4ulBnPsA==;EndpointSuffix=core.windows.net", "geography-friends-manager");
            ContainerClient.CreateIfNotExists();
        }

        public async Task<string> UploadFile(string id, string extension, Stream stream)
        {
            BlobClient = ContainerClient.GetBlobClient($"{id}.{extension}");
            await BlobClient.UploadAsync(stream);
            return BlobClient.Uri.ToString();
        }
    }
}
