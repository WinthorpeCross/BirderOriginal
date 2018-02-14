using System.ComponentModel.DataAnnotations;

namespace Birder2.Models
{
    public class ObservationBird
    {
        public int BirdId { get; set; }
        public Bird Bird { get; set; }
        public int ObervationId { get; set; }
        public Observation Observation { get; set; }

        [Required]
        [Display(Name = "How many?")]
        public int Quantity { get; set; }
    }
}
