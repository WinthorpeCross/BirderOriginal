using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public enum ObservationsFeedFilter
    {
        [Display(Name = "Show Observations from Your Network")]
        UsersNetwork,
        [Display(Name = "Show Your Observations Only")]
        Users,
        [Display(Name = "Show Latest Public Observations")]
        Public
    }
}
