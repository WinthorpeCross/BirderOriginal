using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Models
{
    public class Bird
    {
        //public Bird()
        //{
        //    Observations = new List<Observation>();
        //}

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



        /*
         * Information about the bird...
         * 
         * */

        public ICollection<Observation> Observations { get; set; }
    }
}
