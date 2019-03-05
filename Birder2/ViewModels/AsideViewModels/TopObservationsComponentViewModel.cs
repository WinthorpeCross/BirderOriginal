using System.Collections.Generic;

namespace Birder2.ViewModels
{
    public class TopObservationsComponentViewModel
    {
        public IEnumerable<TopObservationsViewModel> TopObservations { get; set; }
        public IEnumerable<TopObservationsViewModel> TopMonthlyObservations { get; set; }
    }
}
