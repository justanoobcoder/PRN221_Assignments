namespace NguyenHongHiepSignalR.Admin;

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

    public static bool IsAdmin(string email, string password)
    {
        return email == Email && password == Password;
    }

    private static string GetAdminInfoFromJson(string key)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var info = config[$"AdminAccount:{key}"];
        return info is null ? throw new Exception($"Admin account info {key} is not found in appsettings.json") : info;
    }
}