using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NguyenHongHiepWPF.Admin;

public sealed class AdminAccount
{
    public static string Email
    {
        get => GetAdminInfoFromJson("Email");
    }

    public static string Password
    {
        get => GetAdminInfoFromJson("Password");
    }

    private static string GetAdminInfoFromJson(string key)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        return config[$"AdminAccount:{key}"];
    }
}
