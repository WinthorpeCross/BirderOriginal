using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Birder2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public double DefaultLocationLatitude { get; set; }

        public double DefaultLocationLongitude { get; set; }

        [Display(Name = "UserPhoto")]
        public byte[] UserPhoto { get; set; }

        public ICollection<Observation> Observations { get; set; }

        public ICollection<Tag> Tags { get; set; }

        //public ICollection<ApplicationUser> Followers { get; set; }
        //public ICollection<ApplicationUser> Following { get; set; }
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
}
