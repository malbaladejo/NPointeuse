using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace NPointeuse.Services.LocalFile.Impl
{
    internal class LocalSpecificExpectedTimeDataService : ISpecificExpectedTimeDataService
    {
        private readonly IDirectoryManager directoryManager;
        private readonly ISerializer serializer;
        private readonly string filePath;
        private List<SpecificExpectedTime> durations;

        public LocalSpecificExpectedTimeDataService(IDirectoryManager directoryManager, ISerializer serializer)
        {
            this.directoryManager = directoryManager;
            this.serializer = serializer;
            this.filePath = this.GetFilePath();
            this.EnsureDurations();
        }

        public IReadOnlyCollection<SpecificExpectedTime> GetExpectedDurations(DateTime beginDate, DateTime endDate)
            => this.durations.Where(d => beginDate.BeginOfDay() <= d.Date && d.Date <= endDate.EndOfDay()).ToArray();

        private string GetFilePath() => Path.Combine(this.directoryManager.GetFolderPath(), "specific.json");

        private void EnsureDurations()
        {
            if (this.durations != null) return;

            if (!File.Exists(this.filePath))
            {
                this.durations = new List<SpecificExpectedTime>();
                return;
            }            
            
            var localDurations = this.serializer.Deserialize<List<SpecificExpectedTime>>(this.filePath);

            this.durations = localDurations.OrderByDescending(d => d.Date).ToList();
        }

        private void SaveDurations()
        {
            this.serializer.Serialize(this.durations,this.filePath);
        }

        public void Save(SpecificExpectedTime time)
        {
            if (time.Id != null) return;

            time.Id = DateTime.Now.Ticks;
            time.Date = time.Date.BeginOfDay();
            this.durations.Add(time);

            this.SaveDurations();
        }

        public void Delete(SpecificExpectedTime time)
        {
            if (time.Id == null) return;

            this.durations.RemoveAll(t => t.Id == time.Id);

            this.SaveDurations();
        }

        public IReadOnlyCollection<SpecificExpectedTime> GetExpectedDurations(int page, int pageSize)
            => this.durations.Skip(page * pageSize).Take(pageSize).ToArray();
    }
}
