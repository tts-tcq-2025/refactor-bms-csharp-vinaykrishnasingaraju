using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VitalCheckerTests
{
    [TestClass]
    public class CheckerTests
    {
        [TestMethod]
        public void NotOkWhenTemperatureOutOfRange()
        {
            Assert.IsFalse(Checker.VitalsOk(104f, 70, 98));
        }

        [TestMethod]
        public void NotOkWhenPulseOutOfRange()
        {
            Assert.IsFalse(Checker.VitalsOk(98.6f, 120, 98));
        }

        [TestMethod]
        public void NotOkWhenSpo2OutOfRange()
        {
            Assert.IsFalse(Checker.VitalsOk(98.6f, 70, 85));
        }

        [TestMethod]
        public void OkWhenAllVitalsInRange()
        {
            Assert.IsTrue(Checker.VitalsOk(98.6f, 70, 98));
        }

        [TestMethod]
        public void TemperatureEdgeCases()
        {
            Assert.IsTrue(Checker.VitalsOk(95f, 70, 98));
            Assert.IsTrue(Checker.VitalsOk(102f, 70, 98));
            Assert.IsFalse(Checker.VitalsOk(94.9f, 70, 98));
            Assert.IsFalse(Checker.VitalsOk(102.1f, 70, 98));
        }

        [TestMethod]
        public void PulseEdgeCases()
        {
            Assert.IsTrue(Checker.VitalsOk(98.6f, 60, 98));
            Assert.IsTrue(Checker.VitalsOk(98.6f, 100, 98));
            Assert.IsFalse(Checker.VitalsOk(98.6f, 59, 98));
            Assert.IsFalse(Checker.VitalsOk(98.6f, 101, 98));
        }

        [TestMethod]
        public void Spo2EdgeCases()
        {
            Assert.IsTrue(Checker.VitalsOk(98.6f, 70, 90));
            Assert.IsFalse(Checker.VitalsOk(98.6f, 70, 89));
        }
    }
}
