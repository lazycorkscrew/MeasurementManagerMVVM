using NUnit.Framework;
using MeasurementManagerMVVM.Models;
using System;

namespace MeasurementManager.Tests
{
    public class HourIntervalTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test, Category("MustPass")]
        [TestCase (0, 1)] // С полуночи до часу ночи (1 час)
        [TestCase(10, 12)] // С 10-ти часов утра до 12-ти (2 часа)
        [TestCase(0, 24)] // Целые сутки (23 часа, 24-й час не в счёт)
        public void TestHourIntervalSussess(int a, int b)
        {


            Assert.DoesNotThrow(new TestDelegate(() => { HourInterval hi = new HourInterval(a, b); }));
        }

        [Test, Category("MustFail")]
        [TestCase(23, 0)] //С 23 часов до начала следующих суток (1 час)
        [TestCase(1, 1)] //С часу ночи до часу ночи (0 часов)
        [TestCase(0, 0)] //С полуночи до полуночи тех же суток (0 часов)
        public void TestHourIntervalFails(int a, int b)
        {

            Assert.Throws(typeof(ArgumentException), new TestDelegate(() => { HourInterval hi = new HourInterval(a, b); }));
        }
    }
}