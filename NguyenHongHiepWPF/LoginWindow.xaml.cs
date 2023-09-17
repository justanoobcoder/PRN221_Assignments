using BusinessObjects;
using NguyenHongHiepWPF.Constants;
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

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Email and password are required!");
            return;
        }

        try
        {
            if (email == AdminAccount.Email && password == AdminAccount.Password)
            {
                MessageBox.Show("Login successfully as admin!");
                return;
            }

            var user = customerRepository.GetCustomer(email, password);
            if (user == null)
            {
                MessageBox.Show("Email or password is incorrect!");
                return;
            }
            MessageBox.Show("Login successfully!");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return;
        }
    }
}
