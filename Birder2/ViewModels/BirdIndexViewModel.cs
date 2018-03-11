using Birder2.Models;
using System.Collections.Generic;

namespace Birder2.ViewModels
{
    public class BirdIndexViewModel
    {
        public int SelectedBirdId { get; set; }
        public IEnumerable<Bird> BirdsList { get; set; }
        public IEnumerable<Bird> AllBirdsDropDownList { get; set; }
    }
}
