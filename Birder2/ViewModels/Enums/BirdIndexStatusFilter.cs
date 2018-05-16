using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public enum BirdIndexStatusFilter
    {
        [Display(Name = "Show common British bird species")]
        Common = 0,
        [Display(Name = "Show all British bird species")]
        All = 1
    }
}
