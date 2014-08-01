using System;
using Windows.UI.Xaml.Data;

namespace Radio.Converters
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var concrete = value is DateTime ? (DateTime) value : new DateTime();
            return concrete.ToString("HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
