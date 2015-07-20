using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;

namespace Infrastructure.Tests.Converters
{
    [TestClass]
    public class DoubleToRectMultiConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToRect()
        {
            DoubleToRectMultiConverter converter = new DoubleToRectMultiConverter();
            object[] values = {1d, 2d, 10d, 20d};

            Rect? convertedValue = converter.Convert(values, null, null, null) as Rect?;

            Assert.AreEqual(1d, convertedValue.Value.X);
            Assert.AreEqual(2d, convertedValue.Value.Y);
            Assert.AreEqual(10d, convertedValue.Value.Width);
            Assert.AreEqual(20d, convertedValue.Value.Height);

            object[] values2 = { 1d, 2d, 10d, 20d, Visibility.Hidden };

            Rect? convertedValue2 = converter.Convert(values2, null, null, null) as Rect?;

            Assert.AreEqual(0d, convertedValue2.Value.X);
            Assert.AreEqual(0d, convertedValue2.Value.Y);
            Assert.AreEqual(0d, convertedValue2.Value.Width);
            Assert.AreEqual(0d, convertedValue2.Value.Height);
        }
    }
}
