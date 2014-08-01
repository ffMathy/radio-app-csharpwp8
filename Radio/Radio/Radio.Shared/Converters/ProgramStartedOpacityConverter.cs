using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Radio.Converters
{
    public class ProgramStartedOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var concrete = (bool) value;
            return concrete ? 1 : 0.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
