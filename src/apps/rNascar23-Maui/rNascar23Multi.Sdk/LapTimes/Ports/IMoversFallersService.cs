using rNascar23Multi.Sdk.LapTimes.Models;
using System.Collections.Generic;

namespace rNascar23Multi.Sdk.LapTimes.Ports
{
    public interface IMoversFallersService
    {
        IList<PositionChange> GetDriverPositionChanges(LapTimeData lapTimeData);
    }
}
