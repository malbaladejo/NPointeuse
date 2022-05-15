using System;

namespace NPointeuse.Services
{
    public class SpecificExpectedTime
    {
        public long? Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }

        public SpecificExpectedTime Clone()
           => new SpecificExpectedTime
           {
               Id = this.Id,
               Date = this.Date,
               Duration = this.Duration
           };
    }
}