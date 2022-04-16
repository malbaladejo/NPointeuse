﻿using System;
using System.Collections.Generic;

namespace NPointeuse.Services.Mock
{
    internal class SpecificExpectedTimeDataServiceMock : ISpecificExpectedTimeDataService
    {
        private readonly List<SpecificExpectedTime> times = new List<SpecificExpectedTime>();

        public IReadOnlyCollection<SpecificExpectedTime> GetExpectedDurations(DateTime beginDate, DateTime endDate)
        => this.times;

        public void Save(SpecificExpectedTime time)
        {
            if (time.Id != null) return;

            time.Id = DateTime.Now.Ticks;
            this.times.Add(time);
        }
    }
}
