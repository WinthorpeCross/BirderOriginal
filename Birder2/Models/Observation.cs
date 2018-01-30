using System;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [Display(Name = "Date and time")]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ObservationDateTime { get; set; }

        //public Geography LocationGeoTest { get; set; } ---> Not supported in EF Core 2.0
        public string Location { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Note { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        // Photos

        public int BirdId { get; set; }
        public string ApplicationUserId { get; set; }

        public Bird Bird { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}

