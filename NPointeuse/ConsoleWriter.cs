using NPointeuse.Services;
using System;

namespace NPointeuse
{
    internal class ConsoleWriter : IConsoleWriter
    {
        private readonly IConsoleProgressBar consoleProgressBar;

        public ConsoleWriter(IConsoleProgressBar consoleProgressBar)
        {
            this.consoleProgressBar = consoleProgressBar;
        }

        public void WriteHeader()
        {
            WriteLine("***************");
            WriteLine("*  Pointeuse  *");
            WriteLine("***************");
            WriteLine("");
        }
        public void WriteStatus(IBusinessTimeService timeDataService)
        {
            Write("Status: ");

            if (timeDataService.IsRunning())
                WriteGreen("Running");
            else
                WriteRed("Stopped");

            WriteLine("");
        }

        public void WriteQuestion(IBusinessTimeService timeService)
        {
            Write("Enter s to ");
            if (timeService.IsRunning())
                Console.BackgroundColor = ConsoleColor.Red;
            else
                Console.BackgroundColor = ConsoleColor.Green;

            WriteBlack(GetAction(timeService.IsRunning()));

            Console.BackgroundColor = ConsoleColor.Black;

            Write(" q to quite");
            Console.WriteLine("");
        }

        private static string GetAction(bool isRunning)
        => isRunning ? "stop" : "start";

        public void WriteTimes(IBusinessTimeService timeService)
        {
            WriteInlineTimes("Today", timeService.GetTodayDuration(), timeService.GetTodayExpectedTime());
            WriteInlineTimes("Week", timeService.GetCurrentWeekDuration(), timeService.GetCurrentWeekExpectedTime());
            WriteInlineTimes("Two monthes", timeService.GetLastTwoMontesDuration(), timeService.GetLastTwoMonthesExpectedTime());
        }

        private void WriteInlineTimes(string currentTimeLabel, TimeSpan currentTime, TimeSpan expectedTime)
        {
            Write($"{currentTimeLabel}: ");
            WriteTime(currentTime);
            Write($" left: ");
            var leftTime = expectedTime - currentTime;
            WriteRemainingTime(leftTime);
            Console.WriteLine("");
            this.consoleProgressBar.Write(currentTime, expectedTime);
            WriteLine("");
        }

        private static void WriteTime(TimeSpan time)
        {
            Write(FormatTime(time));
        }

        private static void WriteRemainingTime(TimeSpan time)
        {
            if (time.Ticks > 0)
                WriteGreen(FormatTime(time));
            else
                WriteRed(FormatTime(time));
        }

        private static string FormatTime(TimeSpan time)
            => $"{FormatMinutes((int)(time.TotalMinutes / 60))}h {FormatMinutes((int)(time.TotalMinutes % 60))}min";
        private static string FormatMinutes(int minutes)
               => minutes.ToString().PadLeft(2, '0');


        static void WriteLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }
        static void WriteBlack(string message)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(message);
        }
        static void Write(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
        }
        static void WriteRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
        }
        static void WriteGreen(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
        }
    }
}
