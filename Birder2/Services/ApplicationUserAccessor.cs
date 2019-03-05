using Birder2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class ApplicationUserAccessor : IApplicationUserAccessor
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _context;
        public ApplicationUserAccessor(UserManager<ApplicationUser> userManager,
                                            IHttpContextAccessor context)
        {
            _userManager = userManager;
            _context = context;
        }

        public Task<ApplicationUser> GetUser()
        {
            return _userManager.GetUserAsync(_context.HttpContext.User);
        }

        public async Task<string> GetUserId()
        {
            var user = await _userManager.GetUserAsync(_context.HttpContext.User);
            return user.Id;
        }

        public async Task<string> GetUserName()
        {
            var user = await _userManager.GetUserAsync(_context.HttpContext.User);
            return user.UserName;
        }
    }
}
 
