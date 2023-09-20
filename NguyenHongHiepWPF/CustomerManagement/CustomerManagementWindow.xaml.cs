using BusinessObjects;
using NguyenHongHiepWPF.Admin;
using Repositories;
using Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        // Check if the window was closed by code
        bool wasClosedByCode = new StackTrace().GetFrames().Any(x => x.GetMethod()!.Name == "Close");
        if (wasClosedByCode)
        {
            return;
        }

        // Check if the user wants to close the window
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
        // Get the search value
        string value = txtSearchValue.Text.ToLower();

        // Return full list if the search value is empty
        // otherwise, return the filtered list
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
        // Update the list view
        lvCustomers.ItemsSource = Customers;
    }

    private void btnReload_Click(object sender, RoutedEventArgs e)
    {
        Customers = customerRepository.GetAll().ToList();
        lvCustomers.ItemsSource = Customers;
    }

    private void btnCreate_Click(object sender, RoutedEventArgs e)
    {
        InsertOrUpdateCustomerWindow window = new()
        {
            Title = "Create customer",
            IsInsertAction = true,
            CustomerRepository = customerRepository,
        };
        bool? result = window.ShowDialog();
        if (result == true)
        {
            btnReload_Click(sender, e);
        }
    }

    private void btnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedCustomer == null)
        {
            MessageBox.Show(
                "Please select a customer to update",
                "Update customer",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        InsertOrUpdateCustomerWindow window = new()
        {
            Title = "Update customer",
            IsInsertAction = false,
            CustomerRepository = customerRepository,
            Customer = new()
            {
                CustomerId = SelectedCustomer.CustomerId,
                CustomerName = SelectedCustomer.CustomerName,
                Telephone = SelectedCustomer.Telephone,
                Email = SelectedCustomer.Email,
                Password = SelectedCustomer.Password,
                CustomerBirthday = SelectedCustomer.CustomerBirthday,
                CustomerStatus = SelectedCustomer.CustomerStatus,
            },
        };
        bool? result = window.ShowDialog();
        if (result == true)
        {
            btnReload_Click(sender, e);
        }

    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedCustomer == null)
        {
            MessageBox.Show(
                "Please select a customer to delete",
                "Delete customer",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }

        try
        {
            MessageBoxResult boxResult = MessageBox.Show(
                "Are you sure you want to delete this customer?",
                "Delete customer",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (boxResult == MessageBoxResult.Yes)
            {
                customerRepository.Delete(SelectedCustomer);
                btnReload_Click(sender, e);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Delete customer",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
