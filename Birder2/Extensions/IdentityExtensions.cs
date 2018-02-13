using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Birder2.Models
{
    public static class IdentityExtensions
    {
        public static async Task<ApplicationUser> FindByNameOrEmailAsync(this UserManager<ApplicationUser> userManager,
                                                                         string usernameOrEmail, string password)
        {
            var username = usernameOrEmail;
            if (usernameOrEmail.Contains("@"))
            {
                var userForEmail = await userManager.FindByEmailAsync(usernameOrEmail);
                if (userForEmail != null)
                {
                    username = userForEmail.UserName;
                }
            }
            return await userManager.FindByNameOrEmailAsync(username, password);
        }

        //public static ApplicationUser FindByCardIDAsync(this UserManager<ApplicationUser> um, string cardId)
        //{
        //    return um?.Users?.SingleOrDefault(x => x.CardID == cardId);
        //}
    }
}

//public static class UserManagerExtensions
//{
//public static async Task<ApplicationUser> SetUserPhoto(this UserManager<ApplicationUser> um, byte[] photo)
// {
//var user = from 
////Make Async
//var t = um.Users.
//    return um?.Users?.SingleOrDefault(x => x.UserName == "l");
//_dbContext.Observations.Update(observation);
//await _dbContext.SaveChangesAsync();
//return (observation);
//}

//public static ApplicationUser FindByCardIDAsync(this UserManager<ApplicationUser> um, string cardId)
//{
//    return um?.Users?.SingleOrDefault(x => x.CardID == cardId);
//}

//public static ApplicationUser FindByAddressAsync(this UserManager<ApplicationUser> um, string address)
//{
//    return um?.Users?.SingleOrDefault(x => x.Address == address);
//}
// }