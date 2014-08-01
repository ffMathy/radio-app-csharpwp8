using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Radio.Converters
{
    public class RadioWebFeedCollectionVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var concrete = value as IEnumerable<object>;
            return (concrete != null && concrete.Count() > 1) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
