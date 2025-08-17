using Microsoft.VisualStudio.TestTools.UnitTesting;
using healthchecker;
using System;

namespace healthcheckerTests
{
    [TestClass]
    public class CheckerTest
    {
        private string? capturedMessage;

        private void CaptureAlert(string msg)
        {
            capturedMessage = msg;
        }

        [TestInitialize]
        public void Setup()
        {
            capturedMessage = null;
        }

        [TestMethod]
        public void TestTemperatureOutOfRange()
        {
            Assert.IsFalse(Checker.VitalsOk(104, 70, 98, CaptureAlert));
            Assert.AreEqual("Temperature critical!", capturedMessage);
        }

        [TestMethod]
        public void TestPulseOutOfRange()
        {
            Assert.IsFalse(Checker.VitalsOk(98.6, 120, 98, CaptureAlert));
            Assert.AreEqual("Pulse Rate is out of range!", capturedMessage);
        }

        [TestMethod]
        public void TestSpo2OutOfRange()
        {
            Assert.IsFalse(Checker.VitalsOk(98.6, 70, 88, CaptureAlert));
            Assert.AreEqual("Oxygen Saturation out of range!", capturedMessage);
        }

        [TestMethod]
        public void TestAllVitalsInRange()
        {
            Assert.IsTrue(Checker.VitalsOk(98.6, 70, 98, CaptureAlert));
            Assert.IsNull(capturedMessage);
        }

        [TestMethod]
        public void TestTemperatureEdgeCases()
        {
            Assert.IsTrue(Checker.VitalsOk(95, 70, 98, CaptureAlert));
            Assert.IsTrue(Checker.VitalsOk(102, 70, 98, CaptureAlert));
            Assert.IsFalse(Checker.VitalsOk(94.9, 70, 98, CaptureAlert));
            Assert.AreEqual("Temperature critical!", capturedMessage);
            Assert.IsFalse(Checker.VitalsOk(102.1, 70, 98, CaptureAlert));
            Assert.AreEqual("Temperature critical!", capturedMessage);
        }

        [TestMethod]
        public void TestPulseEdgeCases()
        {
            Assert.IsTrue(Checker.VitalsOk(98.6, 60, 98, CaptureAlert));
            Assert.IsTrue(Checker.VitalsOk(98.6, 100, 98, CaptureAlert));
            Assert.IsFalse(Checker.VitalsOk(98.6, 59, 98, CaptureAlert));
            Assert.AreEqual("Pulse Rate is out of range!", capturedMessage);
            Assert.IsFalse(Checker.VitalsOk(98.6, 101, 98, CaptureAlert));
            Assert.AreEqual("Pulse Rate is out of range!", capturedMessage);
        }

        [TestMethod]
        public void TestSpo2EdgeCases()
        {
            Assert.IsTrue(Checker.VitalsOk(98.6, 70, 90, CaptureAlert));
            Assert.IsFalse(Checker.VitalsOk(98.6, 70, 89, CaptureAlert));
            Assert.AreEqual("Oxygen Saturation out of range!", capturedMessage);
        }
    }
}
