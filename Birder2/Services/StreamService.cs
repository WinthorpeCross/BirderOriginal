using ImageMagick;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class StreamService : IStreamService
    {
        public async Task<byte[]> GetByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        // ToDo: Add parameters for the image size.  With overloads.
        // ToDo: Accept and return MemoryStream

        public byte[] ResizePhoto(byte[] resizeArray, int width, int height)
        {
            using (MagickImage image = new MagickImage(resizeArray))
            {
                MagickGeometry size = new MagickGeometry(width, height);

                size.IgnoreAspectRatio = true;

                image.Resize(size);
                //image.Write(@"C:\Users\rcros\Desktop\NewSize.png");
                resizeArray = image.ToByteArray();
            }
            return resizeArray;
        }
    }
}



//public async Task<byte[]> GetByteArray(IFormFile file)
//{
//    using (var memoryStream = new MemoryStream())
//    {
//        await file.CopyToAsync(memoryStream);
//        return memoryStream.ToArray();
//    }
//}


