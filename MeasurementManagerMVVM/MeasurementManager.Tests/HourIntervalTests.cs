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
        [TestCase (0, 1)] // � �������� �� ���� ���� (1 ���)
        [TestCase(10, 12)] // � 10-�� ����� ���� �� 12-�� (2 ����)
        [TestCase(0, 24)] // ����� ����� (23 ����, 24-� ��� �� � ����)
        public void TestHourIntervalSussess(int a, int b)
        {


            Assert.DoesNotThrow(new TestDelegate(() => { HourInterval hi = new HourInterval(a, b); }));
        }

        [Test, Category("MustFail")]
        [TestCase(23, 0)] //� 23 ����� �� ������ ��������� ����� (1 ���)
        [TestCase(1, 1)] //� ���� ���� �� ���� ���� (0 �����)
        [TestCase(0, 0)] //� �������� �� �������� ��� �� ����� (0 �����)
        public void TestHourIntervalFails(int a, int b)
        {

            Assert.Throws(typeof(ArgumentException), new TestDelegate(() => { HourInterval hi = new HourInterval(a, b); }));
        }
    }
}