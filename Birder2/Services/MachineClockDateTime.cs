using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class MachineClockDateTime : IMachineClockDateTime
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}
