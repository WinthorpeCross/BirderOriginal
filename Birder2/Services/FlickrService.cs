using FlickrNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class FlickrService : IFlickrService
    {
        private static string myFlickrApiKey = "7700051a31f80a964a5d0037ad5ed564";
        private static string myFlickrSecret = "59f50feafa488bad";

        public PhotoCollection GetFlickrPhotoCollection(string queryString)
        {
            // ToDo: Make asynchronous, if possible...
            // ToDo: Implement disposable to use the using statement...
            Flickr flickr = new Flickr(myFlickrApiKey, myFlickrSecret);
            {
                var options = new PhotoSearchOptions { Text = queryString, Extras = PhotoSearchExtras.AllUrls, SafeSearch = SafetyLevel.Safe, MediaType = MediaType.Photos };
                return flickr.PhotosSearch(options);
            }
        }
    }
}

/* DOCUMENTATION:
 * 
 * Install-Package FlickrNet -Version 4.0.4-alpha  |  https://www.nuget.org/packages/FlickrNet/4.0.4-alpha
 * Above FlickerNet package for .NET Core.  Not available through NuGet
 * see https://github.com/samjudson/flickr-net for documentation
 * */
//options.SafeSearch = SafetyLevel.Safe;
//options.Licenses.Add(LicenseType.AttributionCC);
//options.MediaType = MediaType.Photos;
//*Tags = "colorful",*/ PerPage = 20, Page = 1 };

