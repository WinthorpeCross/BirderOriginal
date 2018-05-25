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
                //memoryStream.ToArray();

                using (MagickImage image = new MagickImage(memoryStream.ToArray()))
                {
                    MagickGeometry size = new MagickGeometry(64, 64);

                    size.IgnoreAspectRatio = true;

                    image.Resize(size);

                    image.Write(memoryStream);

                    //image.Write(SampleFiles.OutputDirectory + "Snakeware.100x100.png");
                }
                return memoryStream.ToArray();
            }
        }

    }
}





