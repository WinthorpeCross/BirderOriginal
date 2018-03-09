using Birder2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    // ----> add annotations
    public class CreateObservationViewModel
    {
        public CreateObservationViewModel()
        {
            ObservedSpecies = new List<ObservedSpeciesViewModel>();
        }

        public Observation Observation { get; set; }
        public IEnumerable<Bird> Birds { get; set; }
        public List<ObservedSpeciesViewModel> ObservedSpecies { get; set; }
        public bool IsModelStateValid { get; set; }
        public string MessageToClient { get; set; }
    }

    public class ObservedSpeciesViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int BirdId { get; set; }
    }
}