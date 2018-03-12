using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Birder2.Data;
using Birder2.Models;
using Birder2.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Birder2.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser> GetUserAndNetworkAsyncByUserName(ApplicationUser user)
        {
            return await _dbContext.Users
                         .Include(x => x.Followers)
                             .ThenInclude(x => x.Follower)
                         .Include(y => y.Following)
                             .ThenInclude(r => r.ApplicationUser)
                         .Where(x => x.UserName == user.UserName)
                         .FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> GetUserAndNetworkAsyncByUserName(string userName)
        {
            return await _dbContext.Users
                         .Include(x => x.Followers)
                             .ThenInclude(x => x.Follower)
                         .Include(y => y.Following)
                             .ThenInclude(r => r.ApplicationUser)
                         .Where(x => x.UserName == userName)
                         .FirstOrDefaultAsync();


        }

        public async Task<IEnumerable<UserViewModel>> GetFollowingList(ApplicationUser user)
        {
            var followingList = from following in user.Following
                                    select new UserViewModel
                                    {
                                        UserName = following.ApplicationUser.UserName,
                                        ProfileImage = following.ApplicationUser.ProfileImage
                                    };
            return followingList;
        }

        public async Task<IEnumerable<UserViewModel>> GetFollowersList(ApplicationUser user)
        {
            var followerList = from follower in user.Followers
                                  select new UserViewModel
                                  {
                                      UserName = follower.Follower.UserName,
                                      ProfileImage = follower.Follower.ProfileImage
                                  };
            return followerList;
        }



        public void Follow(ApplicationUser loggedinUser, ApplicationUser userToFollow)
        {
            userToFollow.Followers.Add(new Network { Follower = loggedinUser });
            _dbContext.SaveChanges();
        }

        public void UnFollow(ApplicationUser loggedinUser, ApplicationUser userToUnfollow)
        {
            loggedinUser.Following.Remove(userToUnfollow.Followers.FirstOrDefault());
            _dbContext.SaveChanges();
        }
    }
}
