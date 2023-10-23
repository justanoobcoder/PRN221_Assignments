using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validation;

public static class StringValidation
{
    public static bool IsEmail(this string email)
    {
        Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
        return validateEmailRegex.IsMatch(email);
    }

    public static bool IsPhoneNumber(this string phone)
    {
        Regex validatePhoneNumberRegex = new Regex("^\\+?[0-9][0-9]{7,14}$");
        return validatePhoneNumberRegex.IsMatch(phone);
    }

    public static bool IsBlank(this string str)
    {
        return string.IsNullOrEmpty(str.Trim());
    }
}
