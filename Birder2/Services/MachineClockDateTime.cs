using System;

namespace Birder2.Services
{
    public class MachineClockDateTime : IMachineClockDateTime
    {
        public DateTime Now { get { return DateTime.Now; } }
        public DateTime Today { get { return DateTime.Today; } }
    }
}
