using Birder2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    //ToDo: add annotations
    public class CreateObservationViewModel
    {
        public CreateObservationViewModel()
        {
            ObservedSpecies = new List<ObservedSpeciesViewModel>();
        }

        public Observation Observation { get; set; }
        public IEnumerable<Bird> Birds { get; set; }
        public List<ObservedSpeciesViewModel> ObservedSpecies { get; set; } //ToDo: why did I use a List<T> here?
        public bool IsModelStateValid { get; set; }
        public string MessageToClient { get; set; }
        public double DefaultLatitude { get; set; }
        public double DefaultLongitude { get; set; }
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