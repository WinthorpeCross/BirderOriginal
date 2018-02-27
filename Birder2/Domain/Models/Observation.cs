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

        //public Geography LocationGeoTest { get; set; } ---> Not supported in EF Core 2.0
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }

        [Required]
        [Display(Name = "How many?")]
        public int Quantity { get; set; }

        public string NoteGeneral { get; set; }

        public string NoteHabitat { get; set; }
        // Note plant life, water sources and vegetation conditions, as well as which of the plants the bird is preferring as you observe it.

        public string NoteWeather { get; set; }
        // Note temperature, visibility, wind, light level and any weather conditions that affect your observations.Rain, mist, snowfall, accumulated snow, drought and other factors can impact observations.

        public string NoteAppearance { get; set; }
        // Take copious notes on the bird's appearance, including the brilliance of plumage, any peculiar markings and any outstanding or unusual features such as missing feathers, leucistic patches or signs of illness. Also record the bird's gender if possible.
        // Gender

        public string NoteBehaviour { get; set; }
        // Take notes on what the bird was doing as you observed it.Note general actions and specific reactions to changing conditions, such as the appearance of a predator or how the bird interacts with other birds.Note large actions such as preening, flight patterns and foraging habits as well as small movements such as tail bobs, head cocks or wing stretches.

        public string NoteVocalisation { get; set; }
        // If the bird sang or made other sounds during your observation, use mnemonics or descriptions of how it sounded. Also note non-vocal sounds such as wing noises or drumming.


        [Required]
        [Display(Name = "When?")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime ObservationDateTime { get; set; }
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

