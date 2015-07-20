using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;
using System.Windows;

namespace Infrastructure.Tests.Converters
{
    [TestClass]
    public class BoolToVisibilityConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToVisibility()
        {
            BoolToVisibilityConverter converter = new BoolToVisibilityConverter();

            object value = true;
            var convertedValue = converter.Convert(value, typeof(bool), null, null) as Visibility?;
            Assert.AreEqual(Visibility.Visible, convertedValue);

            value = false;
            convertedValue = converter.Convert(value, typeof(bool), null, null) as Visibility?;
            Assert.AreEqual(Visibility.Hidden, convertedValue);
        }
    }
}
