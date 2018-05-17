using System;
using System.Collections.Generic;
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
        public async Task<string> StoreImage(string filename, byte[] image)
        {
            return null;
        }
    }
}
