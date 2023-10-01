using BusinessObjects;
using Repositories;
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

namespace NguyenHongHiepWPF.CustomerWindows;

public partial class CustomerDashboardWindow : Window
{
    public ICustomerRepository CustomerRepository { get; set; } = default!;
    public Customer Customer { get; set; } = default!;

    public CustomerDashboardWindow()
    {
        InitializeComponent();
    }

    private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Customer == null)
                throw new Exception("Customer not found");
            CustomerProfileWindow window = new()
            {
                Customer = Customer,
                IsViewProfile = false,
                CustomerRepository = CustomerRepository,
            };
            window.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void btnViewProfile_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Customer == null)
                throw new Exception("Customer not found");
            CustomerProfileWindow window = new()
            {
                Customer = Customer,
            };
            window.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void btnViewHistory_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            RentingHistoryWindow window = new()
            {
                Customer = Customer,
            };
            window.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
