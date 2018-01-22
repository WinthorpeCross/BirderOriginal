using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Models
{
    public class Observation
    {
        /* 
         * This is for an individual bird observation.
         * Consider functionality for mulitiple observations.
         */

        [Key]
        public int ObservationId { get; set; }


        //Quantity

        [Required]
        [Display(Name = "Date and time")]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ObservationDateTime { get; set; }

        // public double Latitude { get; set; }
        public string Location { get; set; }

        public string Note { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastUpdateDate { get; set; }
        // Photo


        //Foreign keys
        //[Required]
        public int BirdId { get; set; }
        //[Required]
        public string ApplicationUserId { get; set; }

        //Navigation properties
        public Bird Bird { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}

