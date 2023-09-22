using BusinessObjects;
using DataAccessObjects;
using NguyenHongHiepWPF.Admin;
using NguyenHongHiepWPF.CustomerWindows;
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

namespace NguyenHongHiepWPF;

public partial class LoginWindow : Window
{
    private readonly ICustomerRepository customerRepository = new CustomerRepository();

    public LoginWindow()
    {
        InitializeComponent();
    }
    
    private void btnSignIn_Click(object sender, RoutedEventArgs e)
    {
        string email = txtEmail.Text;
        string password = txtPassword.Password;

        // Check if email and password are empty
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Email and password are required!");
            return;
        }

        try
        {
            // Check if email and password are correct for admin
            if (email == AdminAccount.Email && password == AdminAccount.Password)
            {
                // Open admin dashboard
                AdminDashboardWindow adminDashboard = new();
                Close();
                adminDashboard.Show();
            }
            else // Check if email and password are correct for customer
            {
                var user = customerRepository.GetCustomer(email, password);
                if (user == null)
                {
                    MessageBox.Show("Email or password is incorrect!");
                    return;
                }
                // Check if customer account is deleted
                if (user.CustomerStatus == 0)
                {
                    MessageBox.Show("Your account is deleted!");
                    return;
                }
                // Open customer dashboard
                CustomerDashboardWindow customerDashboard = new()
                {
                    Customer = user,
                    CustomerRepository = customerRepository,
                };
                Close();
                customerDashboard.Show();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return;
        }
    }
}
