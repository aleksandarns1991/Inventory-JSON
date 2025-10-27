using System.Globalization;
using System.Windows.Data;

namespace Inventar.Converters
{
    public class IntToStringConverter : IValueConverter
    {
        private int lastValid; 

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            lastValid = (int)value;
            return lastValid.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value.ToString(),out int number) && number > 0)
            {
                lastValid = number;
                return number;
            }

            return lastValid;
        }
    }
}
