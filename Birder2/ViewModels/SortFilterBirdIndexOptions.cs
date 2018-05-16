using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class SortFilterBirdIndexOptions
    {
        public int SelectedBirdId { get; set; }
        public BirdIndexStatusFilter BirdStatusFilter { get; set; }
        public BirdIndexListFormat ListFormat { get; set; }
        public int page { get; set; }
        //public int SelectPageListSize { get; set; } = 12;
        private IEnumerable<int> _pageSizeList;
        public IEnumerable<int> PageSizeList
        {
            get
            {
                return _pageSizeList ?? (_pageSizeList = new List<int> { 12, 24, 36 });
            }
            private set { }
        }
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
