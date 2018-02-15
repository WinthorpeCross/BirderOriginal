using Birder2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class CreateObservationViewModel
    {
        public Observation Observation { get; set; }
        public IEnumerable<Bird> Birds { get; set; }

        private ICollection<Observation> _myObservations;
        public ICollection<Observation> MyOberservations
        {
            get
            {
                return _myObservations ?? (_myObservations = new List<Observation>());
            }
            set
            {
                _myObservations = value;
            }
        }

        //public List<BirdyViewModel> h { get; set; }
        //private ICollection<BirdyViewModel> _courseAssignments;
        //public ICollection<BirdyViewModel> CourseAssignments
        //{
        //    get
        //    {
        //        return _courseAssignments ?? (_courseAssignments = new List<BirdyViewModel>());
        //    }
        //    set
        //    {
        //        _courseAssignments = value;
        //    }
        //}
    }

    //public class BirdyViewModel
    //{
    //    [Required]
    //    public string BrirdName { get; set; }

    //    [Required]
    //    public int Quantity { get; set; }
    //}
}

