using Birder2.Models;
using System;

namespace Birder2.ViewModels
{
    public class DeleteObservationDto
    {
        public int ObservationId { get; set; }
        public int Quantity { get; set; }
        public bool HasPhotos { get; set; }
        public DateTime ObservationDateTime { get; set; }
        public Bird ObservedBird { get; set; }
    }
}
