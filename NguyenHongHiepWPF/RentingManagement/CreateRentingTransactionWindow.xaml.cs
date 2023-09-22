using BusinessObjects;
using Repositories;
using Repositories.Implementations;
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
using NguyenHongHiepWPF.Models;

namespace NguyenHongHiepWPF.RentingManagement;

public partial class CreateRentingTransactionWindow : Window
{
    private readonly IRentingTransactionRepository transactionRepository = new RentingTransactionRepository();
    private readonly ICarInformationRepository carRepository = new CarInformationRepository();
    private readonly ICustomerRepository customerRepository = new CustomerRepository();

    public List<CarInformation> Cars { get; set; } = default!;
    public List<Customer> Customers { get; set; } = default!;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public List<OrderDetail> OrderDetails { get; set; } = new();
    public OrderDetail? SelectedOrderDetail { get; set; } = default!;

    public CreateRentingTransactionWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Cars = carRepository.GetAll()
                .Where(c => c.CarStatus == 1)
                .ToList();
            Customers = customerRepository.GetAll()
                .Where(c => c.CustomerStatus == 1)
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

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (StartDate > EndDate)
            {
                throw new Exception("Start date must be before end date");
            }
            if (StartDate < DateTime.Now)
            {
                throw new Exception("Start date must be after current date");
            }
            if (transactionRepository.CanRentCar(Cars[cbCars.SelectedIndex].CarId, StartDate, EndDate))
            {
                OrderDetails.Add(new OrderDetail
                {
                    CarId = Cars[cbCars.SelectedIndex].CarId,
                    CarName = Cars[cbCars.SelectedIndex].CarName,
                    CustomerId = Customers[cbCustomers.SelectedIndex].CustomerId,
                    CustomerName = Customers[cbCustomers.SelectedIndex].CustomerName!,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    Price = Cars[cbCars.SelectedIndex].CarRentingPricePerDay,
                });
                lvOrderDetails.ItemsSource = OrderDetails;
            }
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
