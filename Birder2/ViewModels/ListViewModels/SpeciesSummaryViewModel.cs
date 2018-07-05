using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class SpeciesSummaryViewModel
    {
        [Display(Name = "Vernacular Name")]
        public string Vernacular { get; set; }
        [Display(Name = "Scientific Name")]
        public string ScientificName { get; set; }
        [Display(Name = "Est. population")]
        public string PopSize { get; set; }
        [Display(Name = "Status in Britain")]
        public string BtoStatus { get; set; }
        [Display(Name = "Conservation List")]
        public string ConservationStatus { get; set; }
        [Display(Name = "Observations Count")]
        public int Count { get; set; }
    }
}
