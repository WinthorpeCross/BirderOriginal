using System.Collections.Generic;

namespace Birder2.ViewModels
{
    //public class SortFilterBirdIndexOptions
    //{
    //    public int SelectedBirdId { get; set; }
    //    public bool ShowAll { get; set; }
    //    public bool ShowInTable { get; set; }
    //}

    public class SortFilterBirdIndexOptions
    {
        public int SelectedBirdId { get; set; }
        public BirdIndexStatusFilter BirdStatusFilter { get; set; }
        public BirdIndexListFormatFilter ListFormat { get; set; }
        public int page { get; set; }
        public int SelectedPageListSize { get; set; } = 12;
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
}
