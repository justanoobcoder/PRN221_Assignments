using BusinessObjects;
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

namespace NguyenHongHiepWPF.RentingManagement;

public partial class RentingDetailsWindow : Window
{
    public RentingTransaction? Transaction { get; set; } = default!;
    public List<RentingDetail> Details { get; set; } = default!;

    public RentingDetailsWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Details = Transaction!.RentingDetails.ToList();
        DataContext = this;
    }
}
