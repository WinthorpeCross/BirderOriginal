using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class SortFilterBirdIndexOptions
    {
        public int SelectedBirdId { get; set; }
        public BirdIndexStatusFilter BirdStatusFilter { get; set; }
        public BirdIndexListFormat ListFormat { get; set; }
        public int page { get; set; }
    }

    public enum BirdIndexStatusFilter
    {
        [Display(Name = "Show common species")]
        Common = 0,
        [Display(Name = "Show all species")]
        All = 1
    }

    public enum BirdIndexListFormat
    {
        [Display(Name = "Thumbnail format")]
        Thumbnail = 0,
        [Display(Name = "Tabular view")]
        Table = 1
    }
}
