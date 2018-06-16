using Birder2.Models;
using Birder2.ViewModels;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IObservationsAnalysisService
    {
        Task<ObservationsAnalysisDto> GetObservationAnalysis(ApplicationUser user);
    }
}
