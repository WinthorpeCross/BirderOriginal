using Birder2.Models;
using System.Collections.Generic;

namespace Birder2.ViewModels
{
    public class ObservationsIndexViewModel
    {
        public IEnumerable<Observation> Observations;
        public bool ShowUserObservationsOnly;
    }
}
