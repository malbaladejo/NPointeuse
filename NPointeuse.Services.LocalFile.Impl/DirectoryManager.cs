using System;
using System.IO;
using System.Reflection;

namespace NPointeuse.Services.LocalFile.Impl
{
    internal class DirectoryManager: IDirectoryManager
    {
        private string currentNPointeuseDataPath;
        public string GetFolderPath()
        {
            if (this.currentNPointeuseDataPath == null)
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var nPointeuseDataPath = Path.Combine("NPointeuse");
                var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                this.currentNPointeuseDataPath = Path.Combine(appDataPath, nPointeuseDataPath, version);

                this.EnsureAppDataFolder(currentNPointeuseDataPath);
            }

            return currentNPointeuseDataPath;
        }

        private void EnsureAppDataFolder(string currentNPointeuseDataPath)
        {
            if (!Directory.Exists(currentNPointeuseDataPath))
                Directory.CreateDirectory(currentNPointeuseDataPath);
        }       
    }
}
