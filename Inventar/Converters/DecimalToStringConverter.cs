using System.Globalization;
using System.Windows.Data;

namespace Inventar.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        private decimal lastValid;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            lastValid = (decimal)value;
            return lastValid.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (decimal.TryParse(value.ToString(),out decimal number) && number > 0M)
            {
                lastValid = number;
                return number;
            }

            return lastValid;
        }
    }
}
