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
        public string MessageToClient { get; set; }
    }

    public class ObservedSpeciesViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int BirdId { get; set; }
    }
}



        //private ICollection<Observation> _myObservations;
        //public ICollection<Observation> MyOberservations
        //{
        //    get
        //    {
        //        return _myObservations ?? (_myObservations = new List<Observation>());
        //    }
        //    set
        //    {
        //        _myObservations = value;
        //    }
        //}



