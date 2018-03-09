using Birder2.Models;
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
        private IEnumerable<ApplicationUser> _searchResults;
        public IEnumerable<ApplicationUser> SearchResults
        {
            get
            {
                return _searchResults ?? (_searchResults = new List<ApplicationUser>());
            }
            set
            {
                _searchResults = value;
            }
        }
    }
}
