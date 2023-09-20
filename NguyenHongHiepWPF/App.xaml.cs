using NguyenHongHiepWPF.CarManagement;
using System.Windows;

namespace NguyenHongHiepWPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        //LoginWindow window = new();
        CarManagementWindow window = new();
        window.Show();
    }
}
