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
                    // This will resize the image to a fixed size without maintaining the aspect ratio.
                    // Normally an image will be resized to fit inside the specified size.
                    size.IgnoreAspectRatio = true;

                    image.Resize(size);

                    image.Write(memoryStream);

                    // Save the result
                    //image.Write(SampleFiles.OutputDirectory + "Snakeware.100x100.png");
                }
                return memoryStream.ToArray();
            }
        }

    }
}





