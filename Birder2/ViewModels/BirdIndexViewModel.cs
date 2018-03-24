using Birder2.Models;
using System.Collections.Generic;

namespace Birder2.ViewModels
{
    public class BirdIndexViewModel
    {
        public bool ShowAllBirds { get; set; }
        public bool ShowInTable { get; set; }
        public int SelectedBirdId { get; set; }
        public PagedResult<Bird> BirdsList { get; set; }
        public IEnumerable<Bird> AllBirdsDropDownList { get; set; }
    }
}
