using Birder2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Birder2.ViewModels
{
    public class ObservationsIndexViewModel
    {
        public PagedResult<Observation> Observations;
        public bool ShowUserObservationsOnly;
    }
}
