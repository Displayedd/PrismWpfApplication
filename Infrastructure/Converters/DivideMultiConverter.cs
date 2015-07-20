using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PrismWpfApplication.Infrastructure.Converters
{
    public class DivideMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts <paramref name="values"/> of double objects into the ratio of
        /// the two first elements of <paramref name="values"/>.
        /// </summary>
        /// <param name="values">Objects to convert.</param>
        /// <param name="targetType">Type of objects in <paramref name="values"/>.</param>
        /// <param name="parameter">Offset subtractet from first element in <paramref name="values"/>.</param>
        /// <param name="culture">Localization information.</param>
        /// <returns>Ratio betweent the two first elements in <paramref name="values"/> as a double.</returns>
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double subtract = 0;
            double offset = 0;
            double lhs = 0;
            double rhs = 1;

            if(values.Length > 2)
                double.TryParse(values[2].ToString(), out offset);

            if(parameter != null)            
                double.TryParse(parameter.ToString(), out subtract);

            if (values.Length > 1)
            {
                double.TryParse(values[0].ToString(), out lhs);
                double.TryParse(values[1].ToString(), out rhs);
                return Math.Abs((lhs - subtract + offset) / rhs);
            }

            return 1d;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
