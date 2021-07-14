using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCatalogAvalonia.Converters
{
    public class TagsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                if (bool.TryParse(parameter as string, out bool isMenu)) 
                {
                    
                    if (strValue.Contains("!"))
                        return strValue.Replace("!", "").ToUpper();
                    else if (isMenu)
                        return new string(' ', 6) + strValue.Replace("!", "").ToLower();
                    return strValue;
                }
                else
                {
                    if (strValue.Contains("!"))
                        return 2;
                    return 1;
                }
            }
            throw new NotSupportedException();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
