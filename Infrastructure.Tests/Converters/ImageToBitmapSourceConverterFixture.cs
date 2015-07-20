using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismWpfApplication.Infrastructure.Converters;
using System.Windows;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Infrastructure.Tests.Converters
{
    [TestClass]
    public class ImageToBitmapSourceConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToBitmapSource()
        {
            ImageToBitmapSourceConverter converter = new ImageToBitmapSourceConverter();

            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream imageStream = assembly.GetManifestResourceStream("Infrastructure.Tests.Resources.Treebark.png");

            object value = Image.FromStream(imageStream);
            BitmapSource convertedValue = converter.Convert(value, typeof(bool), null, null) as BitmapSource;
            Assert.IsNotNull(convertedValue);

            value = null;
            convertedValue = converter.Convert(value, typeof(bool), null, null) as BitmapSource;
            Assert.IsNull(convertedValue);
        }
    }
}
