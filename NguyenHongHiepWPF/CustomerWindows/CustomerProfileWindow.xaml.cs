using BusinessObjects;
using NguyenHongHiepWPF.Admin;
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
using Validation;

namespace NguyenHongHiepWPF.CustomerWindows;

public partial class CustomerProfileWindow : Window
{
    public ICustomerRepository CustomerRepository { get; set; } = default!;
    public Customer Customer { get; set; } = default!;
    public bool IsViewProfile { get; set; } = true;

    public CustomerProfileWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (IsViewProfile)
        {
            txtName.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPassword.IsEnabled = false;
            txtTelephone.IsEnabled = false;
            dpBirthday.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
        }
    }


    private void ValidateFields(string name, string telephone, string password, string email, DateTime? birthday)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name is required");
        if (string.IsNullOrWhiteSpace(telephone))
            throw new Exception("Telephone is required");
        if (!telephone.IsPhoneNumber())
            throw new Exception("Telephone must contain 8-12 digits");
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("Email is required");
        if (!email.IsEmail())
            throw new Exception("Email is invalid");
        if (email == AdminAccount.Email)
            throw new Exception("Email is already used");
        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("Password is required");
        if (password.Length < 6)
            throw new Exception("Password must be at least 6 characters");
        if (dpBirthday.SelectedDate == null)
            throw new Exception("Birthday is required");
        if (DateTime.Now.Year - dpBirthday.SelectedDate.Value.Year < 18)
            throw new Exception("Customer must be at least 18 years old");
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string name = txtName.Text.Trim();
            string telephone = txtTelephone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            DateTime? birthday = dpBirthday.SelectedDate;

            ValidateFields(name, telephone, password, email, birthday);
            
            Customer.CustomerName = name;
            Customer.Telephone = telephone;
            Customer.Email = email;
            Customer.Password = password;
            Customer.CustomerBirthday = birthday!.Value;
            CustomerRepository.Update(Customer);
            MessageBox.Show(
                "Update profile successfully",
                "Information",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            DialogResult = true;
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
