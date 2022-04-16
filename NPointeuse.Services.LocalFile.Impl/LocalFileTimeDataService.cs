using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace NPointeuse.Services.LocalFile.Impl
{
    internal class LocalFileTimeDataService : ITimeDataService
    {
        private readonly string filePath;
        private readonly IDirectoryManager directoryManager;
        private List<DateRange> dateRanges;

        public LocalFileTimeDataService(IDirectoryManager directoryManager)
        {
            this.directoryManager = directoryManager;
            this.filePath = this.GetFilePath();
            this.EnsureDateRanges();
        }

        public IReadOnlyCollection<DateRange> GetDateRangeForPeriod(DateTime beginDateTime, DateTime endDateTime)
            => this.dateRanges.Where(d => d.Overlap(beginDateTime, endDateTime))
                              .ToArray();

        public DateTime? PendingTime() => this.dateRanges.FirstOrDefault(d => d.IsPending())?.BeginDate;

        public void Start()
        {
            var dateRange = new DateRange
            {
                BeginDate = DateTime.Now,
                Id = DateTime.Now.Ticks
            };

            this.dateRanges.Add(dateRange);
            this.SaveDateRanges();
        }

        public void Stop()
        {
            this.dateRanges.First(d => d.IsPending()).EndDate = DateTime.Now;
            this.SaveDateRanges();
        }       

        private string GetFilePath() => Path.Combine(this.directoryManager.GetFolderPath(), "time.json");

        private void EnsureDateRanges()
        {
            if (this.dateRanges != null) return;

            if(!File.Exists(this.filePath))
            {
                this.dateRanges = new List<DateRange>();
                return;
            }

            var json = File.ReadAllText(this.filePath);
            this.dateRanges = JsonSerializer.Deserialize<List<DateRange>>(json,
                new JsonSerializerOptions { WriteIndented = true });
        }

        private void SaveDateRanges()
        {
            File.WriteAllText(this.filePath, JsonSerializer.Serialize(this.dateRanges));
        }
    }
}
