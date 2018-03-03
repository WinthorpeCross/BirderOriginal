using Birder2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class EditObservationViewModel
    {
        public Observation Observation { get; set; }
        public IEnumerable<Bird> Birds { get; set; }
        //public string MessageToClient { get; set; }
    }
}
