using BusinessObjects;
using NguyenHongHiepWPF.RentingManagement;
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

namespace NguyenHongHiepWPF.CustomerWindows;

public partial class RentingHistoryWindow : Window
{
    private readonly IRentingTransactionRepository transactionRepository = new RentingTransactionRepository();

    public Customer Customer { get; set; } = default!;
    public List<RentingTransaction> Rentings { get; set; } = default!;
    public RentingTransaction? SelectedRenting { get; set; } = default!;

    public RentingHistoryWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Rentings = transactionRepository.GetAll()
                .Where(x => x.CustomerId == Customer.CustomerId)
                .ToList();
            DataContext = this;
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

    }

    private void btnReload_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Rentings = transactionRepository.GetAll()
                .Where(x => x.CustomerId == Customer.CustomerId)
                .ToList();
            lvRentings.ItemsSource = Rentings;
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
}
