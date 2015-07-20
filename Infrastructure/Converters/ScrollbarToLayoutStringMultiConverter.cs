using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PrismWpfApplication.Infrastructure.Converters
{
    public class ScrollbarToLayoutStringMultiConverter : DependencyObject, IMultiValueConverter
    {
        #region Dependency properties
        public static readonly DependencyProperty UpperProperty
            = DependencyProperty.Register(
                                  "Upper",
                                  typeof(string),
                                  typeof(ScrollbarToLayoutStringMultiConverter),
                                  new PropertyMetadata("Upper"));

        public static readonly DependencyProperty LowerProperty
            = DependencyProperty.Register(
                                  "Lower",
                                  typeof(string),
                                  typeof(ScrollbarToLayoutStringMultiConverter),
                                  new PropertyMetadata("Lower"));

        public static readonly DependencyProperty DoubleProperty
            = DependencyProperty.Register(
                                  "Double",
                                  typeof(string),
                                  typeof(ScrollbarToLayoutStringMultiConverter),
                                  new PropertyMetadata("Double"));
        /// <summary>
        /// String signifying scrollbar is at its lowest position.
        /// </summary>
        public string Upper
        {
            get { return (string)this.GetValue(UpperProperty); }
            set { this.SetValue(UpperProperty, value); }
        }

        /// <summary>
        /// String signifying scrollbar is at its highest position.
        /// </summary>
        public string Lower
        {
            get { return (string)this.GetValue(LowerProperty); }
            set { this.SetValue(LowerProperty, value); }
        }

        /// <summary>
        /// String signifying the scrollbar is somewhere between 
        /// its lowest and highest positions.
        /// </summary>
        public string Double
        {
            get { return (string)this.GetValue(DoubleProperty); }
            set { this.SetValue(DoubleProperty, value); }
        }
        #endregion

        /// <summary>
        /// The order of attributes in <paramref name="values"/> must be
        /// scrollbar visibility, Scrollbar offset,  scrollbar maximum offset.
        /// Omitted attributes are set to defualt.
        /// Defaults are 0 (double) and Visible (Visibility).
        /// </summary>
        /// <param name="values">Objects to convert.</param>
        /// <param name="targetType">Type of objects in <paramref name="values"/>.</param>
        /// <param name="parameter">Additional parameter.</param>
        /// <param name="culture">Localization information.</param>
        /// <returns>String containing the layout of the scrollbar.</returns>
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            Visibility visibility = Visibility.Hidden;
            double value = 0, maximum = 0;
            ReadValues(values, ref visibility, ref value, ref maximum);

            if (visibility != Visibility.Visible)
                return string.Empty;
            else if (value == maximum)
                return Upper;
            else if (value == 0d)
                return Lower;
            else
                return Double;
        }

        /// <summary>
        /// Helper function to convert object array to typed objects.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="visibility"></param>
        /// <param name="value"></param>
        /// <param name="maximum"></param>
        private void ReadValues(object[] values, ref Visibility visibility, ref double value, ref double maximum)
        {
            if(values.Length > 0)
                Visibility.TryParse(values[0].ToString(), out visibility);
            if (values.Length > 1)
                double.TryParse(values[1].ToString(), out value);
            if (values.Length > 1)
                double.TryParse(values[2].ToString(), out maximum);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
