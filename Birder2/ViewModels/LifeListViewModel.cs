using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class LifeListViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Species")]
        public string Vernacular { get; set; }
        public string ScientificName { get; set; }
        public string PopSize { get; set; }
        public string BtoStatus { get; set; }
        public string ConservationStatus { get; set; }
        public int Count { get; set; }
    }
}
