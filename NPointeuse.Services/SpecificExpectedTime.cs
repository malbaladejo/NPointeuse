using System;

namespace NPointeuse.Services
{
    public class SpecificExpectedTime
    {
        public long? Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
    }
}