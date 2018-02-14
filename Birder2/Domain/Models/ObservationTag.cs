using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Models
{
    public class ObservationTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int ObervationId { get; set; }
        public Observation Observation { get; set; }
    }
}
