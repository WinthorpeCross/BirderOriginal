using Birder2.Models;
using FlickrNet;

namespace Birder2.ViewModels
{
    public class BirdDetailViewModel
    {
        public Bird Bird { get; set; }
        public PhotoCollection BirdPhotos { get; set; }
    }
}
