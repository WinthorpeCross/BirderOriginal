using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IStreamService
    {
        byte[] ResizePhoto(byte[] resizeArray, int width, int height);
        Task<byte[]> GetByteArray(IFormFile file);
    }
}
    
    

