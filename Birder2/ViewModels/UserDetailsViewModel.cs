using Birder2.Models;

namespace Birder2.ViewModels
{
    public class UserDetailsViewModel
    {
        public string UserName { get; set; }
        public byte[] ProfileImage { get; set; }
        public bool IsFollowing { get; set; }
        public int UniqueSpeciesCount { get; set; }
        public PagedResult<Observation> Observations { get; set; }
    }
}
