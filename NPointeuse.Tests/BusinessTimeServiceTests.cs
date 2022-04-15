using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPointeuse.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPointeuse.Services.Tests
{
    [TestClass()]
    public class BusinessTimeServiceTests
    {
        [TestMethod()]
        public void BusinessTimeServiceTest()
        {
            // Arrange
            var service = CreateTimeService();

            // Assert
            Assert.IsNotNull(service);
        }

        [TestMethod()]
        public void GetTodayDurationTest()
        {
            // Arrange
            var timeDataService = Substitute.For<ITimeDataService>();
            timeDataService.GetRealDurationsForDatePeriod(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(new[] {
                    new DateRange{ BeginDate = DateTime.Today.AddHours(8), EndDate = DateTime.Today.AddHours(12) },
                    new DateRange{ BeginDate = DateTime.Today.AddHours(14), EndDate = DateTime.Today.AddHours(18) }});

            var service = CreateTimeService(timeDataService: timeDataService);

            // Act
            var duration = service.GetTodayDuration();

            // Assert
            Assert.AreEqual(TimeSpan.FromHours(8), duration);
            timeDataService.Received(1).GetRealDurationsForDatePeriod(DateTime.Today, DateTime.Today.EndOfDay());
        }

        [TestMethod()]
        public void GetCurrentWeekDurationTest()
        {
            // Arrange
            var timeDataService = Substitute.For<ITimeDataService>();
            timeDataService.GetRealDurationsForDatePeriod(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(new[] {
                    new DateRange{ BeginDate = DateTime.Today.AddHours(8), EndDate = DateTime.Today.AddHours(12) },
                    new DateRange{ BeginDate = DateTime.Today.AddHours(14), EndDate = DateTime.Today.AddHours(18) }});

            var service = CreateTimeService(timeDataService: timeDataService);

            // Act
            var duration = service.GetCurrentWeekDuration();

            // Assert
            Assert.AreEqual(TimeSpan.FromHours(8), duration);
            timeDataService.Received(1).GetRealDurationsForDatePeriod(
                DateTime.Today.FirstDayOfWeek(),
                DateTime.Today.LastDayOfWeek().EndOfDay());
        }

        [TestMethod()]
        public void GetLastTwoMontesDurationTest()
        {
            // Arrange
            var timeDataService = Substitute.For<ITimeDataService>();
            timeDataService.GetRealDurationsForDatePeriod(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(new[] {
                    new DateRange{ BeginDate = DateTime.Today.AddHours(8), EndDate = DateTime.Today.AddHours(12) },
                    new DateRange{ BeginDate = DateTime.Today.AddHours(14), EndDate = DateTime.Today.AddHours(18) }});

            var service = CreateTimeService(timeDataService: timeDataService);

            // Act
            var duration = service.GetLastTwoMontesDuration();

            // Assert
            Assert.AreEqual(TimeSpan.FromHours(8), duration);
            timeDataService.Received(1).GetRealDurationsForDatePeriod(
                DateTime.Today.AddMonths(-2), DateTime.Today.EndOfDay());
        }

        [TestMethod()]
        public void GetTodayExpectedTime_No_Specific_Duration_Test()
        {
            // Arrange
            var specificService = Substitute.For<ISpecificExpectedTimeDataService>();
            specificService.GetExpectedDurations(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(new ISpecificExpectedTime[0]);

            var stardardDurations = new Dictionary<DayOfWeek, TimeSpan>
            {
                [DayOfWeek.Monday] = TimeSpan.FromHours(8),
                [DayOfWeek.Tuesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Wednesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Thursday] = TimeSpan.FromHours(8),
                [DayOfWeek.Friday] = TimeSpan.FromHours(8),
                [DayOfWeek.Saturday] = TimeSpan.FromHours(0),
                [DayOfWeek.Sunday] = TimeSpan.FromHours(0)
            };

            stardardDurations[DateTime.Today.DayOfWeek] = TimeSpan.FromHours(5);
            var standardService = Substitute.For<IStandardExpectedTimeDataService>();
            standardService.GetExpectedDurations().Returns(stardardDurations);

            var service = CreateTimeService(specificService: specificService, standardService: standardService);

            // Act
            var duration = service.GetTodayExpectedTime();

            // Assert
            Assert.AreEqual(TimeSpan.FromHours(5), duration);
            specificService.Received(1).GetExpectedDurations(DateTime.Today, DateTime.Today.EndOfDay());
        }

        [TestMethod()]
        public void GetTodayExpectedTime_Specific_Duration_Test()
        {
            // Arrange
            var specificService = Substitute.For<ISpecificExpectedTimeDataService>();
            var specificTimes = new ISpecificExpectedTime[] { CreateSpecificExpectedTime(DateTime.Today, TimeSpan.FromHours(5)) };

            specificService.GetExpectedDurations(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(specificTimes);

            var service = CreateTimeService(specificService: specificService);

            // Act
            var duration = service.GetTodayExpectedTime();

            // Assert
            Assert.AreEqual(TimeSpan.FromHours(5), duration);
            specificService.Received(1).GetExpectedDurations(DateTime.Today, DateTime.Today.EndOfDay());
        }

        [TestMethod()]
        public void GetCurrentWeekExpectedTime_No_Specific_Duration_Test()
        {
            // Arrange
            var specificService = Substitute.For<ISpecificExpectedTimeDataService>();
            specificService.GetExpectedDurations(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(new ISpecificExpectedTime[0]);

            var stardardDurations = new Dictionary<DayOfWeek, TimeSpan>
            {
                [DayOfWeek.Monday] = TimeSpan.FromHours(8),
                [DayOfWeek.Tuesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Wednesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Thursday] = TimeSpan.FromHours(8),
                [DayOfWeek.Friday] = TimeSpan.FromHours(8),
                [DayOfWeek.Saturday] = TimeSpan.FromHours(0),
                [DayOfWeek.Sunday] = TimeSpan.FromHours(0)
            };

            var standardService = Substitute.For<IStandardExpectedTimeDataService>();
            standardService.GetExpectedDurations().Returns(stardardDurations);

            var service = CreateTimeService(specificService: specificService, standardService: standardService);

            // Act
            var duration = service.GetCurrentWeekExpectedTime();

            // Assert
            Assert.AreEqual(TimeSpan.FromHours(40), duration);
            specificService.Received(1).GetExpectedDurations(DateTime.Today.FirstDayOfWeek(), DateTime.Today.LastDayOfWeek().EndOfDay());
        }

        [TestMethod()]
        public void GetCurrentWeekExpectedTime_Specific_Duration_Test()
        {
            // Arrange
            var specificService = Substitute.For<ISpecificExpectedTimeDataService>();
            var specificTimes = new ISpecificExpectedTime[] { CreateSpecificExpectedTime(DateTime.Today, TimeSpan.FromHours(5)) };

            specificService.GetExpectedDurations(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(specificTimes);

            var service = CreateTimeService(specificService: specificService);

            // Act
            var duration = service.GetCurrentWeekExpectedTime();

            // Assert
            Assert.AreEqual(TimeSpan.FromHours(37), duration);
            specificService.Received(1).GetExpectedDurations(DateTime.Today.FirstDayOfWeek(), DateTime.Today.LastDayOfWeek().EndOfDay());
        }

        [TestMethod()]
        public void GetLastTwoMonthesExpectedTime_No_Specific_Duration_Test()
        {
            // Arrange
            var specificService = Substitute.For<ISpecificExpectedTimeDataService>();
            specificService.GetExpectedDurations(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(new ISpecificExpectedTime[0]);

            var stardardDurations = new Dictionary<DayOfWeek, TimeSpan>
            {
                [DayOfWeek.Monday] = TimeSpan.FromHours(8),
                [DayOfWeek.Tuesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Wednesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Thursday] = TimeSpan.FromHours(8),
                [DayOfWeek.Friday] = TimeSpan.FromHours(8),
                [DayOfWeek.Saturday] = TimeSpan.FromHours(0),
                [DayOfWeek.Sunday] = TimeSpan.FromHours(0)
            };

            var standardService = Substitute.For<IStandardExpectedTimeDataService>();
            standardService.GetExpectedDurations().Returns(stardardDurations);

            var service = CreateTimeService(specificService: specificService, standardService: standardService);

            // Act
            var duration = service.GetLastTwoMonthesExpectedTime();

            // Assert
            specificService.Received(1).GetExpectedDurations(DateTime.Today.AddMonths(-2), DateTime.Today.LastDayOfWeek().EndOfDay());
        }

        [TestMethod()]
        public void GetLastTwoMonthesExpectedTime_Specific_Duration_Test()
        {
            // Arrange
            var specificService = Substitute.For<ISpecificExpectedTimeDataService>();
            var specificTimes = new ISpecificExpectedTime[] { CreateSpecificExpectedTime(DateTime.Today, TimeSpan.FromHours(5)) };

            specificService.GetExpectedDurations(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(specificTimes);

            var service = CreateTimeService(specificService: specificService);

            // Act
            var duration = service.GetLastTwoMonthesExpectedTime();

            // Assert
            specificService.Received(1).GetExpectedDurations(DateTime.Today.AddMonths(-2), DateTime.Today.LastDayOfWeek().EndOfDay());
        }

        [TestMethod()]
        public void IsRunningTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StartTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void StopTest()
        {
            Assert.Fail();
        }

        private static BusinessTimeService CreateTimeService(
            ITimeDataService timeDataService = null,
            IStandardExpectedTimeDataService standardService = null,
            ISpecificExpectedTimeDataService specificService = null)
        {
            timeDataService = timeDataService ?? Substitute.For<ITimeDataService>();
            standardService = standardService ?? CreateStandardExpectedTimeDataService();
            specificService = specificService ?? Substitute.For<ISpecificExpectedTimeDataService>();
            return new BusinessTimeService(timeDataService, standardService, specificService);
        }

        private static IStandardExpectedTimeDataService CreateStandardExpectedTimeDataService()
        {
            var standardService = Substitute.For<IStandardExpectedTimeDataService>();
            standardService.GetExpectedDurations().Returns(new Dictionary<DayOfWeek, TimeSpan>
            {
                [DayOfWeek.Monday] = TimeSpan.FromHours(8),
                [DayOfWeek.Tuesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Wednesday] = TimeSpan.FromHours(8),
                [DayOfWeek.Thursday] = TimeSpan.FromHours(8),
                [DayOfWeek.Friday] = TimeSpan.FromHours(8),
                [DayOfWeek.Saturday] = TimeSpan.FromHours(0),
                [DayOfWeek.Sunday] = TimeSpan.FromHours(0)

            });

            return standardService;
        }

        private static ISpecificExpectedTime CreateSpecificExpectedTime(DateTime date, TimeSpan duration)
        {
            var time = Substitute.For<ISpecificExpectedTime>();
            time.Date.Returns(date);
            time.Duration.Returns(duration);
            return time;
        }
    }
}