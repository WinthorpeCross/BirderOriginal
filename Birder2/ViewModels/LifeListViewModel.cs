using System.Collections.Generic;

namespace Birder2.ViewModels
{
    public class LifeListViewModel
    {
        public IEnumerable<SpeciesSummaryViewModel> LifeList { get; set; }
        public int TotalObservations { get; set; }
        public int TotalSpecies { get; set; }
    }
}
