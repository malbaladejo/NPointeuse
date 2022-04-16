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
        private readonly string filePath;
        private List<SpecificExpectedTime> durations;

        public LocalSpecificExpectedTimeDataService(IDirectoryManager directoryManager)
        {
            this.directoryManager = directoryManager;
            this.filePath = this.GetFilePath();
            this.EnsureDurations();
        }

        public IReadOnlyCollection<SpecificExpectedTime> GetExpectedDurations(DateTime beginDate, DateTime endDate)
            => this.durations.Where(d => beginDate <= d.Date && d.Date <= endDate).ToArray();

        private string GetFilePath() => Path.Combine(this.directoryManager.GetFolderPath(), "specific.json");

        private void EnsureDurations()
        {
            if (this.durations != null) return;

            if (!File.Exists(this.filePath))
            {
                this.durations = new List<SpecificExpectedTime>();
                return;
            }

            var json = File.ReadAllText(this.filePath);
            this.durations = JsonSerializer.Deserialize<List<SpecificExpectedTime>>(json);
        }

        private void SaveDurations()
        {
            File.WriteAllText(this.filePath, JsonSerializer.Serialize(this.durations,
                new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Save(SpecificExpectedTime time)
        {
            if (time.Id != null) return;

            time.Id = DateTime.Now.Ticks;
            this.durations.Add(time);

            this.SaveDurations();
        }
    }
}
