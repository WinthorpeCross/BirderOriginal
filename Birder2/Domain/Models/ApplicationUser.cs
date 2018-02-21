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
        public double DefaultLocationLatitude { get; set; }

        public double DefaultLocationLongitude { get; set; }

        [Display(Name = "Profile Image")]
        public byte[] ProfileImage { get; set; }

        public ICollection<Observation> Observations { get; set; }

        public ICollection<Tag> Tags { get; set; }



        public virtual ICollection<Network> Following { get; set; }
        public virtual ICollection<Network> Followers { get; set; }

    }



    //public class Friendship
    //{
    //    public string Id { get; set; }
    //    public ApplicationUser ApplicationUser { get; set; }

    //    public string FriendId { get; set; }
    //    public ApplicationUser Friend { get; set; }

    //    public StatusCode Status { get; set; }

    //    public string ActionUserId { get; set; }
    //    public ApplicationUser ActionUser { get; set; }

    //    public byte[] Timestamp { get; set; }
    //}

    //public enum StatusCode
    //{
    //    Pending = 0,
    //    Accepted = 1,
    //    Declined = 2,
    //    Blocked = 3
    //}
}

   

