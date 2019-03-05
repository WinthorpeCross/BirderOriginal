using Birder2.Models;
using System.Collections.Generic;

namespace Birder2.ViewModels
{
    public class CreateSingleObservationViewModel
    {
        public Observation Observation { get; set; }
        public IEnumerable<Bird> Birds { get; set; }
        public bool IsModelStateValid { get; set; }
        public string MessageToClient { get; set; }
        public double DefaultLatitude { get; set; }
        public double DefaultLongitude { get; set; }
    }
}
