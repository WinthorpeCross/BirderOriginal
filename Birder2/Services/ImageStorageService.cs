using Birder2.ViewModels;
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
        Task<string> StoreProfileImage(string filename, byte[] image, string containerName);
    }

    public class ImageStorageService : IImageStorageService
    {
        private readonly IConfiguration _config;

        public ImageStorageService(IConfiguration config)
        {
            _config = config;
        }

        //ToDo: Move storage creditials to Azure Key Store...
        public async Task<string> StoreProfileImage(string filename, byte[] image, string containerName)
        {
            var filenameonly = Path.GetFileName(filename);

            var url = string.Concat(_config["BlobStorage:StorageUrl"], containerName, "/", filenameonly);
            var creditials = new StorageCredentials(_config["BlobStorage:Account"], _config["BlobStorage:Key"]);
            var blob = new CloudBlockBlob(new Uri(url), creditials);

            //if (!(await blob.ExistsAsync()))
            //{
                await blob.UploadFromByteArrayAsync(image, 0, image.Length);
            //}

            return url;
        }
    }
}
