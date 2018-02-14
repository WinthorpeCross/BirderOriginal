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

        //public ICollection<ApplicationUser> Followers { get; set; }
        //public ICollection<ApplicationUser> Following { get; set; }
    }
}

   

