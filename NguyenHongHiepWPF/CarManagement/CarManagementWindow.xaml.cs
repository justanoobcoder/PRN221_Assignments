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

namespace NguyenHongHiepWPF.CarManagement;

public partial class CarManagementWindow : Window
{
    private readonly ICarInformationRepository carInformationRepository = new CarInformationRepository();

    public List<CarInformation> Cars { get; set; } = default!;
    public CarInformation? SelectedCar { get; set; } = default!;

    public CarManagementWindow()
    {
        Cars = carInformationRepository.GetAll().ToList();
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
            Cars = carInformationRepository.GetAll().ToList();
        }
        else
        {
            Cars = carInformationRepository.GetAll()
                .Where(c => c.CarName!.ToLower().Contains(value))
                .ToList();
        }
        // Update the list view
        lvCars.ItemsSource = Cars;
    }

    private void btnReload_Click(object sender, RoutedEventArgs e)
    {
        Cars = carInformationRepository.GetAll().ToList();
        lvCars.ItemsSource = Cars;
    }
}
