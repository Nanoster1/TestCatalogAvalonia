using Avalonia.Data.Converters;
using System;
using System.Globalization;
using TestCatalogAvalonia.Models;

namespace TestCatalogAvalonia.Converters
{
    public class UserNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is User user)
                return user.Name;
            throw new NotSupportedException("Value isn\'t User");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
