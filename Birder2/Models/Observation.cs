using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.Models
{
    public class Observation
    {
        // This is for an individual bird observation.  Consider functionality for mulitiple observations (different view model for create multiple observations)
        [Key]
        public int ObservationId { get; set; }

        [Required]
        [Display(Name = "When?")]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ObservationDateTime { get; set; }

        //public Geography LocationGeoTest { get; set; } ---> Not supported in EF Core 2.0
        public string Location { get; set; }  //use instead of separate lat/long if Geography comes available
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }

        [Required]
        [Display(Name = "How many?")]
        public int Quantity { get; set; }

        public string Note { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        [Display(Name = "Observed species")]
        public int BirdId { get; set; }
        public string ApplicationUserId { get; set; }

        public Bird Bird { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<ObservationTag> ObservationTags { get; set; }

    }
}

