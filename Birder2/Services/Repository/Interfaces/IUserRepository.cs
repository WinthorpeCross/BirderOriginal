using Birder2.Models;
using Birder2.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserAndNetworkAsyncByUserName(ApplicationUser user);
        Task<ApplicationUser> GetUserAndNetworkAsyncByUserName(string userName);
        Task<IEnumerable<UserViewModel>> GetFollowingList(ApplicationUser user);
        Task<IEnumerable<UserViewModel>> GetFollowersList(ApplicationUser user);
        void Follow(ApplicationUser loggedinUser, ApplicationUser userToFollow);
        void UnFollow(ApplicationUser loggedinUser, ApplicationUser userToUnfollow);
  
    }
}
