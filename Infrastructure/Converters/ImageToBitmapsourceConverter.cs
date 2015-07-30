using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PrismWpfApplication.Infrastructure.Converters
{
    [ValueConversion(typeof(Image), typeof(BitmapSource))]
    public class ImageToBitmapSourceConverter : IValueConverter
    {
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr value);

        /// <summary>
        /// Converts an System.Drawing.Image to a BitmapSource.
        /// </summary>
        /// <param name="value">Image to convert.</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Submitted value as a Bitmapsource.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Image myImage = value as Image;
            BitmapSource bitmapSource = null;
            if (myImage != null)
            {
                IntPtr bmpPt = IntPtr.Zero;

                try
                {
                    var bitmap = new Bitmap(myImage);
                    bmpPt = bitmap.GetHbitmap();
                    bitmapSource =
                     System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                           bmpPt,
                           IntPtr.Zero,
                           Int32Rect.Empty,
                           BitmapSizeOptions.FromEmptyOptions());

                    bitmapSource.Freeze();
                }
                finally
                {
                    if (bmpPt != IntPtr.Zero)
                        DeleteObject(bmpPt);
                }
            }

            return bitmapSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
