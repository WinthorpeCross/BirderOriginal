using System.ComponentModel.DataAnnotations;

namespace Birder2.Models.ManageViewModels
{
    public class SetLocationViewModel
    {
        [Required]
        public double DefaultLocationLatitude { get; set; }

        [Required]
        public double DefaultLocationLongitude { get; set; }

        public string StatusMessage { get; set; }
    }
}
