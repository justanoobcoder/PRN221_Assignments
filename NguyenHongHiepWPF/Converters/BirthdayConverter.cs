using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NguyenHongHiepWPF.Converters;

public class BirthdayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime birthday)
        {
            return birthday.ToString("dd/MM/yyyy");
        }
        return "";
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string birthday)
        {
            return DateTime.ParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        return null;
    }
}
