using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PrismWpfApplication.Infrastructure.Converters
{
    public class DoubleToOffsetMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts <paramref name="values"/> of double objects into an
        /// offset value. The offset is based on the two first elements of
        /// <paramref name="values"/>.
        /// </summary>
        /// <param name="values">Objects to convert.</param>
        /// <param name="targetType">Type of objects in <paramref name="values"/>.</param>
        /// <param name="parameter">Amount to add to offset.</param>
        /// <param name="culture">Localization information.</param>
        /// <returns>Offset value based on <paramref name="values"/>.</returns>
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double parentSize = 0,  childSize = 0, offset = 0;

            if(values.Length > 0)
                double.TryParse(values[0].ToString(), out parentSize);

            if (values.Length > 1)
                double.TryParse(values[1].ToString(), out childSize);
            if(parameter is string)
                double.TryParse(parameter.ToString(), out offset);

            return (parentSize / 2) + (childSize / 2) + offset;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
