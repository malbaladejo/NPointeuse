using System;
using System.Diagnostics;

namespace NPointeuse.Services
{
    [DebuggerDisplay("{DebugValue}")]
    public class DateRange
    {
        private DateTime beginDate;
        private DateTime? endDate;

        public long Id { get; set; } = DateTime.Now.Ticks;

        public DateTime BeginDate
        {
            get => beginDate;
            set
            {
                if (this.endDate.HasValue && value > this.endDate.Value)
                    throw new ArgumentOutOfRangeException("Begin date must lower than end date.");
                beginDate = value;
            }
        }
        public DateTime? EndDate
        {
            get => endDate; set
            {
                if (value.HasValue && value < this.beginDate)
                    throw new ArgumentOutOfRangeException("End date must greater than begin date.");
                endDate = value;
            }
        }

        public string DebugValue => $"{Id}: {BeginDate.ToString()} - {EndDate?.ToString()}";

        public DateRange Clone()
            => new DateRange
            {
                Id = this.Id,
                BeginDate= this.BeginDate,
                EndDate = this.EndDate
            };
    }
}
