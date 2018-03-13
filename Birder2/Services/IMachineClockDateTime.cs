using System;

namespace Birder2.Services
{
    public interface IMachineClockDateTime
    {
        DateTime Now { get; }
        DateTime Today { get; }
    }
}
