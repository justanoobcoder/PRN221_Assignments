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

namespace NguyenHongHiepWPF.ReportStatistic;

public partial class ReportStatisticWindow : Window
{
    private readonly IRentingTransactionRepository transactionRepository = new RentingTransactionRepository();

    public List<RentingTransaction> Rentings { get; set; } = default!;
    public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);
    public DateTime EndDate { get; set; } = DateTime.Now;
    public int? TotalRenting { get; set; }
    public decimal? TotalPrice { get; set; }

    public ReportStatisticWindow()
    {
        Rentings = transactionRepository.GetAllBetween(StartDate, EndDate)
            .OrderByDescending(r => r.TotalPrice)
            .ToList();
        TotalRenting = Rentings.Count;
        TotalPrice = Rentings.Sum(r => r.TotalPrice);
        InitializeComponent();
        DataContext = this;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        dpStartDate.DisplayDateEnd = DateTime.Now;
        dpEndDate.DisplayDateEnd = DateTime.Now;
    }

    private void btnSearch_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (StartDate > EndDate)
                throw new Exception("Start date must be less than end date.");

            Rentings = transactionRepository.GetAllBetween(StartDate, EndDate)
                .OrderByDescending(r => r.TotalPrice)
                .ToList();
            lbCount.Content = Rentings.Count.ToString();
            lbPrice.Content = Rentings.Sum(r => r.TotalPrice).ToString();
            lvRentings.ItemsSource = Rentings;
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
