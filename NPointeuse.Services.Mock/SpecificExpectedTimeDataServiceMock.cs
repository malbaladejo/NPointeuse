using System;
using System.Collections.Generic;

namespace NPointeuse.Services.Mock
{
    internal class SpecificExpectedTimeDataServiceMock : ISpecificExpectedTimeDataService
    {
        private readonly List<SpecificExpectedTime> times = new List<SpecificExpectedTime>();

        public IReadOnlyCollection<SpecificExpectedTime> GetExpectedDurations(DateTime beginDate, DateTime endDate)
        => this.times;

        public IReadOnlyCollection<SpecificExpectedTime> GetExpectedDurations(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void Save(SpecificExpectedTime time)
        {
            if (time.Id != null) return;

            time.Id = DateTime.Now.Ticks;
            this.times.Add(time);
        }

        public void Delete(SpecificExpectedTime time)
        {
            if (time.Id == null) return;

            this.times.RemoveAll(t=>t.Id == time.Id);
        }
    }
}
