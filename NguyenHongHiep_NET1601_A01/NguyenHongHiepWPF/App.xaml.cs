﻿using NguyenHongHiepWPF.Admin;
using NguyenHongHiepWPF.CarManagement;
using NguyenHongHiepWPF.CustomerManagement;
using NguyenHongHiepWPF.RentingManagement;
using NguyenHongHiepWPF.ReportStatistic;
using System.Windows;

namespace NguyenHongHiepWPF;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        LoginWindow window = new();
        window.Show();
    }
}
