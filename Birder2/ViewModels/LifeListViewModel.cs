using System.Linq;

namespace Birder2.ViewModels
{
    public class LifeListViewModel
    {
        public IQueryable<SpeciesSummaryViewModel> LifeList { get; set; }
        public ObservationsAnalysisDto ObservationsAnalysisDto { get; set; }
        //public int TotalObservations { get; set; }
        //public int TotalSpecies { get; set; }
    }
}
