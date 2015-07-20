using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;
using System.Windows;

namespace Infrastructure.Tests.Converters
{
    [TestClass]
    public class ScrollbarToLayoutStringMultiConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToLayoutString()
        {
            ScrollbarToLayoutStringMultiConverter converter = new ScrollbarToLayoutStringMultiConverter();
            object[] values = {Visibility.Hidden, 0d, 20d};

            var convertedValue = converter.Convert(values, typeof(double), null, null) as string;

            Assert.AreEqual(string.Empty, convertedValue);

            object[] values2 = { Visibility.Visible, 0d, 20d };

            var convertedValue2 = converter.Convert(values2, typeof(double), null, null) as string;

            Assert.AreEqual("Lower", convertedValue2);

            object[] values3 = { Visibility.Visible, 20d, 20d };

            var convertedValue3 = converter.Convert(values3, typeof(double), null, null) as string;

            Assert.AreEqual("Upper", convertedValue3);

            object[] values4 = { Visibility.Visible, 1d, 20d };

            var convertedValue4 = converter.Convert(values4, typeof(double), null, null) as string;

            Assert.AreEqual("Double", convertedValue4);
        }
    }
}
