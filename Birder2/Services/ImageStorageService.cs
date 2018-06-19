using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Birder2.Services
{
    public interface IImageStorageService
    {
        Task<string> StoreProfileImage(string filename, byte[] image, string containerName);
        Task<string> StoreObservationImage(string filename, byte[] image, string containerName);
    }

    public class ImageStorageService : IImageStorageService
    {
        private readonly IConfiguration _config;

        public ImageStorageService(IConfiguration config)
        {
            _config = config;
        }


        // ToDo: Consider clearing the cache when the profile picture is updated...
        public async Task<string> StoreProfileImage(string filename, byte[] image, string containerName)
        {
            var filenameonly = Path.GetFileName(filename);
            var url = string.Concat(_config["BlobStorage:StorageUrl"], containerName, "/", filenameonly);
            var creditials = new StorageCredentials(_config["BlobStorage:Account"], _config["BlobStorageKey"]);
            var blob = new CloudBlockBlob(new Uri(url), creditials);

            await blob.UploadFromByteArrayAsync(image, 0, image.Length);

            return url;
        }

        //public static async Task<bool> UploadFileToStorage(Stream fileStream, string fileName, AzureStorageConfig _storageConfig)
        public async Task<bool> UploadFileToStorage(Stream fileStream, string fileName)
        {
            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(_config["BlobStorage:Account"], _config["BlobStorageKey"]);

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference("test");

            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Upload the file
            await blockBlob.UploadFromStreamAsync(fileStream);

            return await Task.FromResult(true);
        }


        public async Task<string> StoreObservationImage(string filename, byte[] image, string containerName)
        {
            /*
             * Create container if it does not exist
             */

            //CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            //// Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 
            //var cloudBlobContainer = cloudBlobClient.GetContainerReference("quickstartblobs" + Guid.NewGuid().ToString());
            //await cloudBlobContainer.CreateAsync();


            //var blobClient = blob.CreateCloudBlobClient();
            //var blobContainer = blobClient.GetContainerReference(blobContainerName);
            //await blobContainer.CreateIfNotExistsAsync();



            var filenameonly = Path.GetFileName(filename);
            var url = string.Concat(_config["BlobStorage:StorageUrl"], containerName, "/", filenameonly);
            var creditials = new StorageCredentials(_config["BlobStorage:Account"], _config["BlobStorageKey"]);
            var blob = new CloudBlockBlob(new Uri(url), creditials);

            if (!(await blob.ExistsAsync()))
            {
                await blob.UploadFromByteArrayAsync(image, 0, image.Length);
            }

            return url;
        }
    }
}
