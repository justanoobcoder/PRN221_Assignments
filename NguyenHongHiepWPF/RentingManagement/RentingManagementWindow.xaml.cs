using BusinessObjects;
using NguyenHongHiepWPF.Admin;
using Repositories;
using Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace NguyenHongHiepWPF.RentingManagement;

public partial class RentingManagementWindow : Window
{
    private readonly IRentingTransactionRepository transactionRepository = new RentingTransactionRepository();

    public List<RentingTransaction> Rentings { get; set; } = default!;
    public RentingTransaction? SelectedRenting { get; set; } = default!;

    public RentingManagementWindow()
    {
        Rentings = transactionRepository.GetAll().ToList();
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

    private void btnReload_Click(object sender, RoutedEventArgs e)
    {
        Rentings = transactionRepository.GetAll().ToList();
        lvRentings.ItemsSource = Rentings;
    }

    private void btnSearch_Click(object sender, RoutedEventArgs e)
    {
        // Get the search value
        string value = txtSearchValue.Text.ToLower();

        // Return full list if the search value is empty
        // otherwise, return the filtered list
        if (string.IsNullOrEmpty(value))
        {
            Rentings = transactionRepository.GetAll().ToList();
        }
        else
        {
            Rentings = transactionRepository.GetAll()
                .Where(rt => rt.Customer.CustomerName!.ToLower().Contains(value))
                .ToList();
        }
        // Update the list view
        lvRentings.ItemsSource = Rentings;
    }

    private void btnViewDetail_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedRenting == null)
        {
            MessageBox.Show(
                "Please select a renting transaction to view its details",
                "View details",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }
        try
        {
            RentingDetailsWindow window = new()
            {
                Transaction = SelectedRenting,
            };
            window.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "View details",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void btnCreate_Click(object sender, RoutedEventArgs e)
    {
        CreateRentingTransactionWindow window = new();
        window.ShowDialog();
    }
}
