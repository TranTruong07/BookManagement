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
    /// Interaction logic for AddOrEditCustomer.xaml
    /// </summary>
    public partial class AddOrEditCustomer : Window
    {
        public Customer CurrentCustomer { get; set; }
        private readonly ICustomerService customerService;
        public bool IsEditMode { get; set; }
        public AddOrEditCustomer()
        {
            InitializeComponent();
            CurrentCustomer = new Customer();
            IsEditMode = false;
            customerService = new CustomerService(new CustomerRepository());
        }
        public AddOrEditCustomer(Customer customer)
        {
            InitializeComponent();
            customerService = new CustomerService(new CustomerRepository());
            CurrentCustomer = customer;
            IsEditMode = true;
            txtFullName.Text = CurrentCustomer.FullName;
            txtEmail.Text = CurrentCustomer.Email;
            txtPhone.Text = CurrentCustomer.Phone;
            txtAddress.Text = CurrentCustomer.Address;
            dpRegistrationDate.SelectedDate = CurrentCustomer.RegistrationDate;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFullName.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Full Name and Email are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CurrentCustomer.FullName = txtFullName.Text;
            CurrentCustomer.Email = txtEmail.Text;
            CurrentCustomer.Phone = txtPhone.Text;
            CurrentCustomer.Address = txtAddress.Text;
            CurrentCustomer.RegistrationDate = dpRegistrationDate.SelectedDate;

            if(!IsEditMode)
            {
                int result = customerService.AddCustomer(CurrentCustomer);
                if (result == 0)
                {
                    MessageBox.Show("Add Customer Fail", "Add Customer Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                int result = customerService.UpdateCustomer(CurrentCustomer);
                if (result == 0)
                {
                    MessageBox.Show("Update Customer Fail", "Update Customer Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
