using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class FollowUserViewModel
    {
        [Required(ErrorMessage = "The search term must not be blank")]
        [Display(Name = "Search by username (type search term):")]
        public string SearchCriterion { get; set; }
        public string StatusMessage { get; set; }
        private IEnumerable<UserViewModel> _searchResults;
        public IEnumerable<UserViewModel> SearchResults
        {
            get
            {
                return _searchResults ?? (_searchResults = new List<UserViewModel>());
            }
            set
            {
                _searchResults = value;
            }
        }
    }
}
