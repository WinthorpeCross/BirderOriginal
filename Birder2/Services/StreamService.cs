using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class StreamService : IStream
    {
        public async Task<byte[]> GetByteArray(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
    //replaces this:
    //using (var memoryStream = new MemoryStream())
    //{
    //    await model.UserPhoto.CopyToAsync(memoryStream);
    //    user.UserPhoto = memoryStream.ToArray();
    //}




