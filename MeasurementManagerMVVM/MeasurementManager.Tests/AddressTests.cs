using System;
using NUnit.Framework;
using MeasurementManagerMVVM.Models;

namespace MeasurementManager.Tests
{
    public class AddressTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test, Category("MustPass")]
        
        [TestCase("1;2;3;4;5")]
        [TestCase("Москва;ул;Пушкина;1;2")]
        [TestCase("Москва;;Пушкина;1;")]
        public void AddressSussess(string stringAddress)
        {
            Assert.DoesNotThrow(new TestDelegate(() => { Address address = new Address(stringAddress); }));
        }

        [Test, Category("MustFail")]

        [TestCase("")]
        [TestCase(";")]
        [TestCase(";;")]
        [TestCase(";;;")]
        [TestCase(";;;;")]
        [TestCase(";;;;;")]
        [TestCase(";1;2;3;4")]
        [TestCase("1;2;3;4;5;")]
        [TestCase(";1;2;3;4;5")]
        [TestCase(";1;2;3;4;5;")]
        [TestCase("1;2;3;4")]
        [TestCase("1;2;3")]
        [TestCase("1;2")]
        [TestCase("1")]
        [TestCase(";ул;Пушкина;1;2")]
        [TestCase("Москва;ул;;1;2")]
        [TestCase("Москва;ул;Пушкина;;2")]
        [TestCase(";ул;;;2")]
        [TestCase(";;Пушкина;1;2")]
        public void AddressFails(string stringAddress)
        {

            Assert.Throws(typeof(ArgumentException), new TestDelegate(() => { Address address = new Address(stringAddress); }));
        }
    }
}
