using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public enum BirdIndexListFormatFilter
    {
        [Display(Name = "Thumbnail format")]
        Thumbnail = 0,
        [Display(Name = "Tabular view")]
        Table = 1
    }
}
