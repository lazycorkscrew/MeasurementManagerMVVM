using NUnit.Framework;
using MeasurementManagerMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeasurementManager.Tests
{
    public class MeasuringRequestTests
    {
        TownDateLimits TownDateLimit;
        MeasuringRequest currentMeasuring;

        [SetUp]
        public void Setup()
        {
            TownDateLimit = TownDateLimits.GetTestLimits().ToList()[1];
            currentMeasuring = MeasuringRequest.GetMeasurings().ToList()[0];
        }

        [Test, Category("MustPass")]
        public void TestAppoint()
        {

            var current = TownDateLimit.Limits[0].MeasurementsLimit;
            var expect = 1; //Был 1 свободный замер
            Assert.That(current == expect);

            currentMeasuring.Appoint(DateTime.Now, TownDateLimit.Limits[0]);

            current = TownDateLimit.Limits[0].MeasurementsLimit;
            expect = 0; //Должно стать 0 свободных замеров
            Assert.That(current == expect);
        }

        [Test, Category("MustPass")]
        public void TestCancelAppoint()
        {
            currentMeasuring.Appoint(DateTime.Now, TownDateLimit.Limits[0]);

            var current = TownDateLimit.Limits[0].MeasurementsLimit;
            var expect = 0;
            Assert.That(current == expect);

            currentMeasuring.CancelAppoint();

            current = TownDateLimit.Limits[0].MeasurementsLimit;
            expect = 1;
            Assert.That(current == expect);
        }

    }
}