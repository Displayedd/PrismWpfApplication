using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;

namespace Infrastructure.Tests.Converters
{
    [TestClass]
    public class OffsetConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToDouble()
        {
            OffsetConverter converter = new OffsetConverter();
            object value = 20d;
            object param = 5d;

            var convertedValue = converter.Convert(value, typeof(double), param, null) as double?;

            Assert.AreEqual(15d / 20d, convertedValue);
        }
    }
}
