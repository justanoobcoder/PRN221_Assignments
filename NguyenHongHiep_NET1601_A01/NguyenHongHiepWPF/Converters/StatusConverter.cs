using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NguyenHongHiepWPF.Converters;

public class StatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        byte status = (byte)value;
        return status switch
        {
            1 => "Active",
            0 => "Deleted",
            _ => "Unknown",
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string status = (string)value;
        return status switch
        {
            "Active" => 1,
            "Deleted" => 0,
            _ => 2,
        };
    }
}
