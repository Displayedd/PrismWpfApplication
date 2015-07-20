using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;

namespace Infrastructure.Tests.Converters
{
    [TestClass]
    public class DivideMultiConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToDouble()
        {
            DivideMultiConverter converter = new DivideMultiConverter();
            object[] values = {15d, 20d};

            var convertedValue = converter.Convert(values, typeof(double), null, null) as double?;

            Assert.AreEqual(15d / 20d, convertedValue);

            object[] values2 = { 15d, 20d };

            var convertedValue2 = converter.Convert(values2, typeof(double), 5d, null) as double?;

            Assert.AreEqual((15d - 5d) / 20d, convertedValue2);

            object[] values3 = { 15d, 20d, 5d };

            var convertedValue3 = converter.Convert(values3, typeof(double), 5d, null) as double?;

            Assert.AreEqual((15d) / 20d, convertedValue3);
        }
    }
}
