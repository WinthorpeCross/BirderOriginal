using System;

namespace Birder2.Services
{
    public class MachineClockDateTime : IMachineClockDateTime
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}
