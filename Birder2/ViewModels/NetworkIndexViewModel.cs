using System.Collections.Generic;

namespace Birder2.ViewModels
{
    public class NetworkIndexViewModel
    {
        public string StatusMessage { get; set; }
        private IEnumerable<UserViewModel> _followersList;
        public IEnumerable<UserViewModel> FollowersList
        {
            get
            {
                return _followersList ?? (_followersList = new List<UserViewModel>());
            }
            set
            {
                _followersList = value;
            }
        }

        private IEnumerable<UserViewModel> _followingList;
        public IEnumerable<UserViewModel> FollowingList
        {
            get
            {
                return _followingList ?? (_followingList = new List<UserViewModel>());
            }
            set
            {
                _followingList = value;
            }
        }
    }
}
