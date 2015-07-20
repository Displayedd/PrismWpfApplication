using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;

namespace Infrastructure.Tests
{
    [TestClass]
    public class EqualsMultiConverterFixture
    {
        [TestMethod]
        public void ShouldConvertArrayToBoolean()
        {
            EqualsMultiConverter converter = new EqualsMultiConverter();
            object[] values = {20d,19d};

            var convertedValue = converter.Convert(values, typeof(double), null, null) as bool?;

            Assert.AreEqual(false, convertedValue);

            object[] values2 = { 20d, 20d };

            convertedValue = converter.Convert(values2, typeof(double), null, null) as bool?;
            Assert.AreEqual(true, convertedValue);


            object[] values3 = { 20d };

            convertedValue = converter.Convert(values3, typeof(double), null, null) as bool?;
            Assert.AreEqual(true, convertedValue);
        }
    }
}
