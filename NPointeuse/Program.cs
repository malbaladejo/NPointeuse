using NPointeuse.Services;
using System;

namespace NPointeuse
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Initialize();
            
            var timeService = container.GetInstance<IBusinessTimeService>();
            var consoleWriter = container.GetInstance<IConsoleWriter>();
            var entry = string.Empty;

            do
            {
                consoleWriter.WriteHeader();
                consoleWriter.WriteStatus(timeService);
                consoleWriter.WriteTimes(timeService);
                consoleWriter.WriteQuestion(timeService);
                entry = Console.ReadLine();
                ToggleTimeService(timeService, entry);
                Console.Clear();
            }
            while (entry != "q");
        }       

        private static void ToggleTimeService(IBusinessTimeService timeService, string entry)
        {
            if (entry != "s") return;

            if (timeService.IsRunning())
                timeService.Stop();
            else
                timeService.Start();
        }

        
        
    }
}
