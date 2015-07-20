using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;

namespace Infrastructure.Tests.Converters
{
    [TestClass]
    public class DoubleToOffsetMultiConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToOffset()
        {
            DoubleToOffsetMultiConverter converter = new DoubleToOffsetMultiConverter();
            object[] values = {10d, 20d};

            double? convertedValue = converter.Convert(values, null, null, null) as double?;

            Assert.AreEqual(15d, convertedValue);

            object[] values2 = {10d, 20d};

            convertedValue = converter.Convert(values2, null, "5", null) as double?;

            Assert.AreEqual(20d, convertedValue);
        }
    }
}
