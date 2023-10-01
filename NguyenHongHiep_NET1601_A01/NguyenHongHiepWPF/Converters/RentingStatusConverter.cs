using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NguyenHongHiepWPF.Converters;

public class RentingStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        byte status = (byte)value;
        return status switch
        {
            1 => "Renting",
            0 => "Returned",
            _ => throw new NotImplementedException(),
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string status = (string)value;
        return status switch
        {
            "Renting" => 1,
            "Returned" => 0,
            _ => throw new NotImplementedException(),
        };
    }
}
