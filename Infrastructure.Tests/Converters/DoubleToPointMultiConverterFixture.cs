using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;
using System.Windows;

namespace Infrastructure.Tests.Converters
{
    [TestClass]
    public class DoubleToPointConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToPoint()
        {
            DoubleToPointMultiConverter converter = new DoubleToPointMultiConverter();
            object[] values = {15d, 20d};

            var convertedValue = converter.Convert(values, typeof(double), null, null) as Point?;

            Assert.AreEqual(new Point(15, 20), convertedValue);

            object[] values2 = { 15d};

            var convertedValue2 = converter.Convert(values2, typeof(double), null, null) as Point?;

            Assert.AreEqual(new Point(15, 0), convertedValue2);

            object[] values3 = { 15d, 20d };

            var convertedValue3 = converter.Convert(values3, typeof(double), "-1", null) as Point?;

            Assert.AreEqual(new Point(14, 19), convertedValue3);
        }
    }
}
