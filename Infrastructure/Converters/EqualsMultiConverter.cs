using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PrismWpfApplication.Infrastructure.Converters
{
    public class EqualsMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts the submitted object array into a boolean object based on
        /// the equality between the objects in the array.
        /// </summary>
        /// <param name="values">Objects to convert.</param>
        /// <param name="targetType">Type of objects in <paramref name="values"/>.</param>
        /// <param name="parameter">Additional parameter.</param>
        /// <param name="culture">Localization information.</param>
        /// <returns>Boolean true if all objects in <paramref name="values"/> are equal.</returns>
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (values.Length > 1)
            {
                for (int i = 0; i + 1 < values.Length; i++)
                {
                    if (!object.Equals(values[i], values[i + 1]))
                        return false;
                }
            }

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
