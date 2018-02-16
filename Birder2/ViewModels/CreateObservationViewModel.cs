using Birder2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class CreateObservationViewModel
    {
        public Observation Observation { get; set; }
        public IEnumerable<Bird> Birds { get; set; }
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



