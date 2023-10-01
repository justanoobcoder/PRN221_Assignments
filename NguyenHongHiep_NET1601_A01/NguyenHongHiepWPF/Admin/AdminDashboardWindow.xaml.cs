using NguyenHongHiepWPF.CarManagement;
using NguyenHongHiepWPF.CustomerManagement;
using NguyenHongHiepWPF.RentingManagement;
using NguyenHongHiepWPF.ReportStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NguyenHongHiepWPF.Admin;

public partial class AdminDashboardWindow : Window
{
    public AdminDashboardWindow()
    {
        InitializeComponent();
    }

    private void btnCustomer_Click(object sender, RoutedEventArgs e)
    {
        CustomerManagementWindow window = new();
        Close();
        window.Show();
    }

    private void btnCar_Click(object sender, RoutedEventArgs e)
    {
        CarManagementWindow window = new();
        Close();
        window.Show();
    }

    private void btnRenting_Click(object sender, RoutedEventArgs e)
    {
        RentingManagementWindow window = new();
        Close();
        window.Show();
    }

    private void btnReport_Click(object sender, RoutedEventArgs e)
    {
        ReportStatisticWindow window = new();
        window.ShowDialog();
    }
}
