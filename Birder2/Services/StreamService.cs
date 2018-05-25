using ImageMagick;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class StreamService : IStream
    {
        public async Task<byte[]> GetByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                using (MagickImage image = new MagickImage(memoryStream.ToArray()))
                {
                    MagickGeometry size = new MagickGeometry(64, 64);

                    size.IgnoreAspectRatio = true;

                    image.Resize(size);

                    image.Write(memoryStream);
                }
                return memoryStream.ToArray();
            }
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


