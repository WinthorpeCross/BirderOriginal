using Birder2.Models;

namespace Birder2.ViewModels
{
    public class ObservationsIndexViewModel
    {
        public PagedResult<Observation> Observations;
        public bool ShowUserObservationsOnly;
    }
}
