using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NPointeuse.Services;
using System.Threading;
using System.Globalization;

namespace NPointeuse.Tests
{
    [TestClass]
    public class DateExtensionsTest
    {
        [TestMethod]
        public void BeginOfDay_Test()
        {
            // Arrange
            var date = DateTime.Now.BeginOfDay();

            // Assert
            Assert.AreEqual(DateTime.Today, date);
        }

        [TestMethod]
        public void EndOfDay_Test()
        {
            // Arrange
            var date = DateTime.Now.EndOfDay();

            // Assert
            Assert.AreEqual(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59), date);
        }

        [TestMethod]
        public void FirstDayOfWeek_Wednesday_en_Culture_Test()
        {
            // Arrange
            var culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            // Act
            var date = new DateTime(2022, 3, 30, 15, 2, 55).FirstDayOfWeek();
            Thread.CurrentThread.CurrentCulture = culture;

            // Assert
            Assert.AreEqual(new DateTime(2022, 3, 27, 15, 2, 55), date);
        }

        [TestMethod]
        public void FirstDayOfWeek_Wednesday_fr_Culture_Test()
        {
            // Arrange
            var culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            // Act
            var date = new DateTime(2022, 3, 30, 15, 2, 55).FirstDayOfWeek();
            Thread.CurrentThread.CurrentCulture = culture;

            // Assert
            Assert.AreEqual(new DateTime(2022, 3, 28, 15, 2, 55), date);
        }

        [TestMethod]
        public void LastDayOfWeek_Wednesday_en_Culture_Test()
        {
            // Arrange
            var culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            // Act
            var date = new DateTime(2022, 3, 30, 15, 2, 55).LastDayOfWeek();
            Thread.CurrentThread.CurrentCulture = culture;

            // Assert
            Assert.AreEqual(new DateTime(2022, 4, 2, 15, 2, 55), date);
        }

        [TestMethod]
        public void LastDayOfWeek_Wednesday_fr_Culture_Test()
        {
            // Arrange
            var culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

            // Act
            var date = new DateTime(2022, 3, 30, 15, 2, 55).LastDayOfWeek();
            Thread.CurrentThread.CurrentCulture = culture;

            // Assert
            Assert.AreEqual(new DateTime(2022, 4, 3, 15, 2, 55), date);
        }
    }
}
