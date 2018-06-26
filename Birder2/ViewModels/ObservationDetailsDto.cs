using Birder2.Models;

namespace Birder2.ViewModels
{
    public class ObservationDetailsDto
    {
        public Observation SelectedObservation { get; set; }
        public bool IsObservationOwner { get; set; }
    }
}
