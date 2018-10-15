using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project4;

namespace CalculatorTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestZeroYear15()
        {
            //Arrange
            double mort = CalcMortgage.ComputeMonthlyPayment(0, 15, .25);
            //Act
            //Assert
            Assert.AreEqual(0, mort);

        }
        [TestMethod]
        public void TestZeroYear30()
        {
            //Arrange
            double mort = CalcMortgage.ComputeMonthlyPayment(0, 30, 1.25);
            //Act
            //Assert
            Assert.AreEqual(0, mort);

        }
        [TestMethod]
        public void TestZeroYearOther()
        {
            //Arrange
            double mort = CalcMortgage.ComputeMonthlyPayment(0, 45, 2.25);
            //Act
            //Assert
            Assert.AreEqual(0, mort);

        }
        [TestMethod]
        public void TestThousandYearOther()
        {
            //Arrange
            double mort = CalcMortgage.ComputeMonthlyPayment(1000, 15, 1.25);
            //Act
            double f = (double)mort;
            //Assert
            Assert.AreNotEqual(6.21, mort);

        }
    }
}
