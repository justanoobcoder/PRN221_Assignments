using BusinessObjects;
using NguyenHongHiepWPF.Admin;
using Repositories;
using Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace NguyenHongHiepWPF.CustomerManagement;

public partial class CustomerManagementWindow : Window
{
    private readonly ICustomerRepository customerRepository = new CustomerRepository();

    public AdminDashboardWindow AdminDashboardWindow { get; set; } = default!;
    public List<Customer> Customers { get; set; } = default!;
    public Customer? SelectedCustomer { get; set; }

    public CustomerManagementWindow()
    {
        Customers = customerRepository.GetAll().ToList();
        InitializeComponent();
        DataContext = this;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        bool wasClosedByCode = new StackTrace().GetFrames().Any(x => x.GetMethod()!.Name == "Close");
        if (wasClosedByCode)
        {
            return;
        }

        MessageBoxResult boxResult = MessageBox.Show(
            "Are you sure you want to exit?",
            "Exit",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        if (boxResult == MessageBoxResult.No)
        {
            e.Cancel = true;
        }
    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        AdminDashboardWindow adminDashboardWindow = new();
        Close();
        adminDashboardWindow.Show();
    }

    private void btnSearch_Click(object sender, RoutedEventArgs e)
    {
        string value = txtSearchValue.Text.ToLower();
        if (string.IsNullOrEmpty(value))
        {
            Customers = customerRepository.GetAll().ToList();
        }
        else
        {
            Customers = customerRepository.GetAll()
                .Where(c => c.CustomerName!.ToLower().Contains(value))
                .ToList();
        }
        lvCustomers.ItemsSource = Customers;
    }

    private void btnReload_Click(object sender, RoutedEventArgs e)
    {
        Customers = customerRepository.GetAll().ToList();
        lvCustomers.ItemsSource = Customers;
    }
}
