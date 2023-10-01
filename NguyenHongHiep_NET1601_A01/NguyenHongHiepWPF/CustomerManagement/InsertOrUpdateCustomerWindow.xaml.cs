using BusinessObjects;
using NguyenHongHiepWPF.Admin;
using Repositories;
using System;
using System.Windows;
using Validation;

namespace NguyenHongHiepWPF.CustomerManagement;

public partial class InsertOrUpdateCustomerWindow : Window
{
    public ICustomerRepository CustomerRepository { get; set; } = default!;
    public bool IsInsertAction { get; set; } = true;
    public Customer Customer { get; set; } = default!;

    public InsertOrUpdateCustomerWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // Minimum age is 18
            dpBirthday.DisplayDateEnd = DateTime.Now.AddYears(-18);

            tblkTitle.Text = (IsInsertAction ? "Create" : "Update") + " Customer";
            
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

            if (IsInsertAction)
            {
                Customer = new()
                {
                    CustomerName = name,
                    Telephone = telephone,
                    Email = email,
                    Password = password,
                    CustomerBirthday = birthday!.Value,
                };
                CustomerRepository.Create(Customer);
                MessageBox.Show(
                    "Create customer successfully",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                Customer.CustomerName = name;
                Customer.Telephone = telephone;
                Customer.Email = email;
                Customer.Password = password;
                Customer.CustomerBirthday = birthday!.Value;
                CustomerRepository.Update(Customer);
                MessageBox.Show(
                    "Update customer successfully",
                    "Information",
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
