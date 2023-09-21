using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace NguyenHongHiepWPF.CarManagement;

public partial class InsertOrUpdateCarWindow : Window
{
    public bool IsInsertAction { get; set; } = true;
    public ICarInformationRepository CarInformationRepository { get; set; } = default!;
    public CarInformation? Car { get; set; } = default!;
    public List<Manufacturer> Manufacturers { get; set; } = default!;
    public List<Supplier> Suppliers { get; set; } = default!;

    public InsertOrUpdateCarWindow()
    {
        InitializeComponent();
    }

    private void NumberOfDoorsValidation(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void SeatingCapacityValidation(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void YearValidation(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void PricePerDayValidation(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Manufacturers = CarInformationRepository.GetAllManufacturers().ToList();
            Suppliers = CarInformationRepository.GetAllSuppliers().ToList();
            tblkTitle.Text = (IsInsertAction ? "Create" : "Update") + " Car";
            if (IsInsertAction)
            {
                cbManufacturer.SelectedIndex = 0;
                cbSupplier.SelectedIndex = 0;
            }
            else
            {
                cbManufacturer.SelectedIndex = Manufacturers.IndexOf(Manufacturers
                    .Where(m => m.ManufacturerId == Car!.ManufacturerId).Single());
                cbSupplier.SelectedIndex = Suppliers.IndexOf(Suppliers
                    .Where(s => s.SupplierId == Car!.SupplierId).Single());
            }
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

    private void ValidateFields(string name, string description, int numberOfDoors, int seatingCapacity,
        string fuelType, int year, decimal pricePerDay)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name is required");
        if (string.IsNullOrWhiteSpace(description))
            throw new Exception("Description is required");
        if (numberOfDoors < 2 || numberOfDoors > 6)
            throw new Exception("Number of doors must be between 2 and 6");
        if (seatingCapacity < 2 || seatingCapacity > 10)
            throw new Exception("Seating capacity must be between 2 and 10");
        if (string.IsNullOrWhiteSpace(fuelType))
            throw new Exception("Fuel type is required");
        if (year < 2000 || year > DateTime.Now.Year)
            throw new Exception("Year must be between 2000 and current year");
        if (pricePerDay <= 0)
            throw new Exception("Price per day must be greater than 0");
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string name = txtName.Text.Trim();
            string desctiption = txtDescription.Text.Trim();
            if (!int.TryParse(txtNumberOfDoors.Text, out int numberOfDoors))
                throw new Exception("Number of doors must be a number");
            if (!int.TryParse(txtSeatingCapacity.Text, out int seatingCapacity))
                throw new Exception("Seating capacity must be a number");
            string fuelType = txtFuelType.Text.Trim();
            if (!int.TryParse(txtYear.Text, out int year))
                throw new Exception("Year must be a number");
            if (!decimal.TryParse(txtPricePerDay.Text, out decimal pricePerDay))
                throw new Exception("Price per day must be a number");
            int manufacturerId = Manufacturers[cbManufacturer.SelectedIndex].ManufacturerId;
            int supplierId = Suppliers[cbSupplier.SelectedIndex].SupplierId;

            ValidateFields(name, desctiption, numberOfDoors, seatingCapacity, fuelType, year, pricePerDay);

            if (IsInsertAction)
            {
                Car = new()
                {
                    CarName = name,
                    CarDescription = desctiption,
                    NumberOfDoors = numberOfDoors,
                    SeatingCapacity = seatingCapacity,
                    FuelType = fuelType,
                    CarRentingPricePerDay = pricePerDay,
                    ManufacturerId = manufacturerId,
                    SupplierId = supplierId,
                    Year = year,
                };
                CarInformationRepository.Create(Car);
                MessageBox.Show(
                    "Create car successfully",
                    "Create car",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                Car!.CarName = name;
                Car.CarDescription = desctiption;
                Car.NumberOfDoors = numberOfDoors;
                Car.SeatingCapacity = seatingCapacity;
                Car.FuelType = fuelType;
                Car.CarRentingPricePerDay = pricePerDay;
                Car.ManufacturerId = manufacturerId;
                Car.SupplierId = supplierId;
                Car.Year = year;
                CarInformationRepository.Update(Car);
                MessageBox.Show(
                    "Update car successfully",
                    "Update car",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                DialogResult = true;
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
