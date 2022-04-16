using System;

namespace NPointeuse.Services
{
    public class DateRange
    {
        private DateTime beginDate;
        private DateTime? endDate;

        public long Id { get; set; }

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

        private DateTime CurrentEndDate
            => this.EndDate.HasValue ? this.EndDate.Value : DateTime.Now;

        public TimeSpan GetDuration()
            => this.CurrentEndDate - this.BeginDate;
    }
}
