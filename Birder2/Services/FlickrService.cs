using FlickrNet;
using Microsoft.Extensions.Configuration;

namespace Birder2.Services
{
    public class FlickrService : IFlickrService
    {
        private readonly IConfiguration _config;

        public FlickrService(IConfiguration config)
        {
            _config = config;
        }

        public PhotoCollection GetFlickrPhotoCollection(string queryString)
        {
            // ToDo: Make asynchronous, if possible...
            // ToDo: Implement disposable to use the using statement...
            Flickr flickr = new Flickr(_config["FlickrApiKey"],_config["FlickrSecret"]);
            {
                var options = new PhotoSearchOptions {
                        Text = queryString,
                        Extras = PhotoSearchExtras.AllUrls,
                        SafeSearch = SafetyLevel.Safe,
                        MediaType = MediaType.Photos };
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

