using System;
using MeasurementManagerMVVM.Models;
//using ut =Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;

namespace MeasureManager.Tests
{
    [TestFixture]
    public class HourIntervalTests
    {
        public void TestMethod(int a, int b)
        {
            HourInterval hi = new HourInterval(a, b);
        }

        [Test]
        [TestCase(0, 23)]
        [TestCase(2, 2)]
        [TestCase(2, 1)]
        [TestCase(2, 6)]
        [TestCase(23, 23)]
        public void CreateCorrectIntervals(int a, int b)
        {
            Assert.DoesNotThrow(() => { HourInterval hi = new HourInterval(a, b); });
        }
        
    }
}
