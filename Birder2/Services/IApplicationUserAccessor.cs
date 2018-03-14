using Birder2.Models;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IApplicationUserAccessor
    {
        Task<ApplicationUser> GetUser();
        Task<string> GetUserName();
        Task<string> GetUserId();
    }
}
