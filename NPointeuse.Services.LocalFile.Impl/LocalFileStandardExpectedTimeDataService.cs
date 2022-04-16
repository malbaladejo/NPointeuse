using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NPointeuse.Services.LocalFile.Impl
{
    internal class LocalFileStandardExpectedTimeDataService : IStandardExpectedTimeDataService
    {
        private readonly IDirectoryManager directoryManager;
        private readonly string filePath;
        private IReadOnlyDictionary<DayOfWeek, TimeSpan> durations;

        public LocalFileStandardExpectedTimeDataService(IDirectoryManager directoryManager)
        {
            this.directoryManager = directoryManager;
            this.filePath = this.GetFilePath();
            this.EnsureDurations();
        }
        public IReadOnlyDictionary<DayOfWeek, TimeSpan> GetExpectedDurations()
            => this.durations;

        private string GetFilePath() => Path.Combine(this.directoryManager.GetFolderPath(), "standard.json");

        private void EnsureDurations()
        {
            if (this.durations != null) return;

            if (!File.Exists(this.filePath))
            {
                EnsureDefaultDurations();
                return;
            }

            var json = File.ReadAllText(this.filePath);
            this.durations = JsonSerializer.Deserialize<Dictionary<DayOfWeek, TimeSpan>>(json);
        }

        private void EnsureDefaultDurations()
        {
            this.durations = new Dictionary<DayOfWeek, TimeSpan>
            {
                [DayOfWeek.Monday] = TimeSpan.FromHours(8),
                [DayOfWeek.Tuesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Wednesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Thursday] = TimeSpan.FromHours(8),
                [DayOfWeek.Friday] = TimeSpan.FromHours(8),
                [DayOfWeek.Saturday] = TimeSpan.FromHours(0),
                [DayOfWeek.Sunday] = TimeSpan.FromHours(0)
            };
            this.SaveDurations();
        }

        private void SaveDurations()
        {
            File.WriteAllText(this.filePath, JsonSerializer.Serialize(this.durations,
                new JsonSerializerOptions { WriteIndented = true })); ;
        }

        public void Save(IReadOnlyDictionary<DayOfWeek, TimeSpan> durations)
        {
            this.durations = durations;
            this.SaveDurations();
        }
    }
}
