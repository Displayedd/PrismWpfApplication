using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PrismWpfApplication.Infrastructure.Converters
{
    public class DoubleToRectMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts the submitted double array into a Rect object.
        /// The order of attributes in the array is X, Y , Width, Height,
        /// Vsibility.
        /// Omitted attributes are set to defualt.
        /// Defaultws are 0 and Visible.
        /// </summary>
        /// <param name="values">Objects to convert.</param>
        /// <param name="targetType">Type of objects in <paramref name="values"/>.</param>
        /// <param name="parameter">Additional parameter.</param>
        /// <param name="culture">Localization information.</param>
        /// <returns>Rect based on <paramref name="values"/>.</returns>
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double x = 0, y = 0, w = 0, h = 0;
            Visibility visibility = Visibility.Visible;

            if (values.Length > 0)
                double.TryParse(values[0].ToString(), out x);
            if (values.Length > 1)
                double.TryParse(values[1].ToString(), out y);
            if (values.Length > 2)
                double.TryParse(values[2].ToString(), out w);
            if (values.Length > 3)
                double.TryParse(values[3].ToString(), out h);
            if (values.Length > 4)
                Visibility.TryParse(values[4].ToString(), out visibility);
            
            return (Visibility.Visible == visibility) ? 
                new Rect(x, y, w, h) :  new Rect();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            Rect? rectangle = value as Rect?;
            object[] values = { 0d, 0d, 0d, 0d };
            if (rectangle != null)
            {
                values[0] = rectangle.Value.X;
                values[1] = rectangle.Value.Y;
                values[2] = rectangle.Value.Width;
                values[3] = rectangle.Value.Height;
            }
            return values;
        }
    }
}
