using System;

namespace NPointeuse
{
    internal class ConsoleProgressBar : IConsoleProgressBar
    {
        private const int numberOfStar = 50;
        public void Write(TimeSpan currentTime, TimeSpan expectedTime)
        {
            var currentMinutes = currentTime.TotalMinutes;
            var expectedMinutes = expectedTime.TotalMinutes;

            for (int i = 0; i < this.GetNumberOfGreenStars(currentMinutes, expectedMinutes); i++)
                WriteGreen("@");

            for (int i = 0; i < this.GetNumberOfWhiteStars(currentMinutes, expectedMinutes); i++)
                Write(".");

            for (int i = 0; i < this.GetNumberOfRedStars(currentMinutes, expectedMinutes); i++)
                WriteRed("X");

            var total = this.GetNumberOfGreenStars(currentMinutes, expectedMinutes)
                + this.GetNumberOfWhiteStars(currentMinutes, expectedMinutes)
                + this.GetNumberOfRedStars(currentMinutes, expectedMinutes);

            Console.WriteLine("");
        }

        private double GetNumberOfGreenStars(double currentMinutes, double expectedMinutes)
        {
            if (IsExceeding(currentMinutes, expectedMinutes))
            {
                var totalMinutes =  currentMinutes;
                return expectedMinutes * numberOfStar / totalMinutes;
            }
            else
                return currentMinutes * numberOfStar / expectedMinutes;
        }

        private double GetNumberOfWhiteStars(double currentMinutes, double expectedMinutes)
        {
            if (IsExceeding(currentMinutes, expectedMinutes))
                return 0;

            return (expectedMinutes - currentMinutes) * numberOfStar / expectedMinutes;

        }

        private double GetNumberOfRedStars(double currentMinutes, double expectedMinutes)
        {
            if (!IsExceeding(currentMinutes, expectedMinutes))
                return 0;

            var totalMinutes =  currentMinutes;
            var overdue = currentMinutes - expectedMinutes;
            return overdue * numberOfStar / totalMinutes;
        }

        private static bool IsExceeding(double currentMinutes, double expectedMinutes)
            => currentMinutes > expectedMinutes;

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
