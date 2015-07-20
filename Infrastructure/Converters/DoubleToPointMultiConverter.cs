using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PrismWpfApplication.Infrastructure.Converters
{
    public class DoubleToPointMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts <paramref name="values"/> of double objects into a
        /// point object where the first element represents x coordinate and
        /// the second element represents the y coordinate.
        /// Omitted coordinates are set to 0.
        /// </summary>
        /// <param name="values">Objects to convert.</param>
        /// <param name="targetType">Type of objects in <paramref name="values"/>.</param>
        /// <param name="parameter">Amount to add to each coordinate.</param>
        /// <param name="culture">Localization information.</param>
        /// <returns>Point with x,y coordinates based on <paramref name="values"/>.</returns>
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double x = 0, y = 0, offset = 0;

            if(values.Length > 0)
                double.TryParse(values[0].ToString(), out x);

            if (values.Length > 1)
                double.TryParse(values[1].ToString(), out y);
            if(parameter is string)
                double.TryParse(parameter.ToString(), out offset);

            return new Point(x + offset, y + offset);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is Point)
                return new object[] { ((Point)value).X, ((Point)value).Y };
            else
                return new object[] { 0d, 0d };
        }
    }
}
