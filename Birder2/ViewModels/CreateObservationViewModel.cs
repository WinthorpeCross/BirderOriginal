using Birder2.Models;
using System.Collections.Generic;

namespace Birder2.ViewModels
{
    public class CreateObservationViewModel
    {
        public Observation Observation { get; set; }
        public IEnumerable<Bird> Birds { get; set; }
    }
}
