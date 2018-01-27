using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Models
{

    //public enum ConserverationFlag
    //{
    //    Amber,
    //    Introduced,
    //    Green,
    //    Red,
    //    Other
    //}

    public class BirdConserverationStatus
    {
        [Key]
        public int BirdConserverationStatusId { get; set; }

        [Required]
        public string ConservationStatus { get; set; }

        public string Note { get; set; }

        public ICollection<Bird> Birds { get; set; }
    }

    public class Bird
    {
        [Key]
        public int BirdId { get; set; }

        //public class ProductImage
        //{
        //    public int ProductId { get; private set; }
        //    public byte[] Image { get; set; }
        //}

        public byte[] Image { get; set; }

        // Species Names
        [Required]
        [Display(Name = "English Name")]
        public string EnglishName { get; set; }

        [Display(Name = "International Name")]
        public string InternationalName { get; set; }

        [Required]
        [Display(Name = "Scientific Name")]
        public string ScientificName { get; set; }



        public int BirdConserverationStatusId { get; set; }

        public ICollection<Observation> Observations { get; set; }

        public BirdConserverationStatus BirdConserverationStatus { get; set; }
    }
}
