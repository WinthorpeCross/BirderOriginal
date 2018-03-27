﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [RegularExpression(@"^[^0-9\\s]+$")] //----> No spaces!  Perhaps?
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ObservationTag> ObservationTags { get; set; }

        /* ToDo: See this webpage for example Tags
         * https://www.mynewsdesk.com/uk/govia-thameslink-railway/pressreleases/gtr-passengers-ride-uks-first-self-drive-mainline-train-2458340
         */


        //public string ApplicationUserId { get; set; }

        //public ApplicationUser ApplicationUser { get; set; }
    }

}
