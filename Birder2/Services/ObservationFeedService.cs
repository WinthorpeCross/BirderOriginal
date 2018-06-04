//using Birder2.Data;
//using Birder2.Models;
//using System.Collections.Generic;
//using System.Linq;

//namespace Birder2.Services
//{
//    public class ViewModel
//    {
//        public List<ObservationFeedItem> Listy { get; set; }
//    }
//    public class ObservationFeedItem
//    {
//        public string observation { get; set; }
//        public int PhotographsCount { get;set; }
//        public bool IsLifer { get; set; }
//        public string ThumbnailUrl { get; set; }
//        //Tags
//    }
//    public class ObservationFeedService
//    {
//        private readonly ApplicationDbContext _dbContext;
//        public ObservationFeedService(ApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public IQueryable<ViewModel> GetUsersObservationFeedItems()
//        {
//            ViewModel j = new ViewModel();
//            var f = (from observation in _dbContext.Observations
//                     select new ObservationFeedItem
//                     {
//                         observation = observation.NoteBehaviour,
//                         PhotographsCount = observation.Photographs.Count(),
//                         IsLifer = true,
//                         ThumbnailUrl = observation.NoteGeneral,
//                     });

//            j.Listy = f.ToList();

//            return 


//        //Tags
//        //Observation = { not everything: what, when, where, whom }
//        //     PhotographsCount =
//        //     Tags =
//        //     IsLifer =
//        //     ThumbnailUrl = { Flickr API or cache }
//            }
//        }

//        /* ******************************************************************
// *         
//    IQueryable  ---->

//    select new ObservationFeedItem
//         {
//             Observation = { not everything: what, when, where, whom }
//             PhotographsCount =
//             Tags = 
//             IsLifer =
//             ThumbnailUrl = { Flickr API or cache }
//         }
//*
//*/
//    }
//}
