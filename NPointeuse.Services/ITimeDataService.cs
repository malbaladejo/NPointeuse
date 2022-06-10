using System;
using System.Collections.Generic;

namespace NPointeuse.Services
{
    public interface ITimeDataService {
        DateRange GetFirstDateRange();
        
        IReadOnlyCollection<DateRange> GetDateRangeForPeriod(DateTime beginDateTime, DateTime endDateTime);

        IReadOnlyCollection<DateRange> GetDateRanges(int page, int pageSize);

        void Start();
        void Stop();

        void Save(DateRange dateRange);

        void Delete(DateRange dateRange);

        DateTime? PendingTime();
    }
}
