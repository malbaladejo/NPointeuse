using System;
using System.Collections.Generic;

namespace NPointeuse.Services
{
    public interface ITimeDataService {
        IReadOnlyCollection<DateRange> GetDateRangeForPeriod(DateTime beginDateTime, DateTime endDateTime);
        void Start();
        void Stop();

        DateTime? PendingTime();
    }
}
