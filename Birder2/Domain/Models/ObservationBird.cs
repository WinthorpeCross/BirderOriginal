using Birder2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Models
{
    public class ObservationBird
    {
        public int BirdId { get; set; }
        public Bird Bird { get; set; }
        public int ObervationId { get; set; }
        public Observation Observation { get; set; }
    }
}
