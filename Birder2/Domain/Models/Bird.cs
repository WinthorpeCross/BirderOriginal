using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.Models
{
    public class ConserverationStatus
    {
        [Key]
        public int ConserverationStatusId { get; set; }

        [Required]
        public string ConservationStatus { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public ICollection<Bird> Birds { get; set; }
    }

    public class BritishStatus
    {
        // - https://www.bto.org/about-birds/birdfacts/british-list
        // - This list includes 603 species (as at 1 January 2017)
        
        [Key]
        public int BritishStatusId { get; set; }

        [Required]
        [Display(Name = "BTO Status in Britain")]
        public string BtoStatusInBritain { get; set; }

        [Required]
        [Display(Name = "Status in Britain")]
        public string BirderStatusInBritain { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public ICollection<Bird> Birds { get; set; }
    }

    public class Bird
    {
        [Key]
        public int BirdId { get; set; }

        [Required]
        public string Class { get; set; } //ALL = AVES

        [Required]
        public string Order { get; set; }

        [Required]
        public string Family { get; set; }

        [Required]
        public string Genus { get; set; }

        [Required]
        [Display(Name = "Scientific Name")]
        public string Species { get; set; }

        [Required]
        [Display(Name = "English Vernacular Name")]
        public string EnglishName { get; set; }

        [Display(Name = "International Name")]
        public string InternationalName { get; set; }

        public string Category { get; set; } //Primary only

        [Display(Name = "Population Size in Britain")]
        public string PopulationSize { get; set; }

        [Url]
        public string ThumbnailUrl { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        //

        public int ConserverationStatusId { get; set; }

        public int BritishStatusId { get; set; }

        public ICollection<Observation> Observations { get; set; }

        public ConserverationStatus BirdConserverationStatus { get; set; }

        public BritishStatus BritishStatus { get; set; }

        public TweetDay TweetDay { get; set; }
    }
}
