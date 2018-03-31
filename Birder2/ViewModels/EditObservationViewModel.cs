using Birder2.Models;
using System.Collections.Generic;

namespace Birder2.ViewModels
{
    public class EditObservationViewModel
    {
        public Observation Observation { get; set; }
        public IEnumerable<Bird> Birds { get; set; }
        public bool IsModelStateValid { get; set; }
        public string MessageToClient { get; set; }
    }
}
