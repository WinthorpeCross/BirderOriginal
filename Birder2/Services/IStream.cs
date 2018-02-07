using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IStream
    {
        Task<byte[]> GetPic(IFormFile file);
    }
}
    
    

