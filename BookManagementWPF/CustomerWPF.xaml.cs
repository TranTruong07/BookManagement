using DataAccess.Models;
using Repositories.Implement;
using Services;
using Services.Implement;
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

namespace BookManagementWPF
{
    /// <summary>
    /// Interaction logic for CustomerWPF.xaml
    /// </summary>
    public partial class CustomerWPF : Window
    {
        private readonly ICustomerService _customerService;
        public CustomerWPF()
        {
            InitializeComponent();
            _customerService = new CustomerService(new CustomerRepository());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text.ToLower();
            string email = txtEmail.Text.ToLower();
            string phone = txtPhone.Text.ToLower();

            var filteredList = _customerService.GetAllCustomer().Where(c =>
                (string.IsNullOrEmpty(fullName) || c.FullName.ToLower().Contains(fullName)) &&
                (string.IsNullOrEmpty(email) || c.Email.ToLower().Contains(email)) &&
                (string.IsNullOrEmpty(phone) || c.Phone?.ToLower().Contains(phone) == true)
            ).ToList();

            dgCustomers.ItemsSource = filteredList;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            dgCustomers.ItemsSource = _customerService.GetAllCustomer();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomer();
        }
        private void LoadCustomer()
        {
            try
            {
                dgCustomers.ItemsSource = null;
                dgCustomers.ItemsSource = _customerService.GetAllCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to load Category " + ex.Message, "Fail");
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditCustomer addCustomerWindow = new AddOrEditCustomer();
            if (addCustomerWindow.ShowDialog() == true)
            {
                dgCustomers.ItemsSource = _customerService.GetAllCustomer();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Customer? selectedCustomer = dgCustomers.SelectedItem as Customer;
            if (selectedCustomer == null)
            {
                MessageBox.Show("Please select a customer to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AddOrEditCustomer addCustomerWindow = new AddOrEditCustomer(selectedCustomer);
            if (addCustomerWindow.ShowDialog() == true)
            {
                dgCustomers.Items.Refresh();
            }
        }
    }
}
