using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IImageStorageService
    {
        Task<string> StoreImage(string filename, byte[] image);
    }

    public class ImageStorageService : IImageStorageService
    {
        private readonly IConfiguration _config;

        public ImageStorageService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> StoreImage(string filename, byte[] image)
        {
            var filenameonly = Path.GetFileName(filename);

            var url = string.Concat(_config["BlobStorage:StorageUrl"], filenameonly);
            var creds = new StorageCredentials(_config["BlobStorage:Account"], _config["BlobStorage:Key"]);
            var blob = new CloudBlockBlob(new Uri(url), creds);

            if (!(await blob.ExistsAsync()))
            {
                await blob.UploadFromByteArrayAsync(image, 0, image.Length);
            }

            return url;
        }
    }
}
