using FlickrNet;

namespace Birder2.Services
{
    public interface IFlickrService
    {
        PhotoCollection GetFlickrPhotoCollection(string queryString);
    }
}
