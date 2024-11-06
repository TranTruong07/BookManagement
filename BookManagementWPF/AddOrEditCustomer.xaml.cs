using DataAccess.Models;
using Repositories.Implement;
using Services;
using Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            string Error;
            if(!Validate(out Error, CurrentCustomer.Email, CurrentCustomer.Phone, CurrentCustomer.Address, CurrentCustomer.RegistrationDate))
            {
                MessageBox.Show(Error, "Add Customer Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
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

        public bool Validate(out string validationErrors, string Email, string Phone, string Address, DateTime? RegistrationDate)
        {
            validationErrors = string.Empty;

            // Validate Email
            if (string.IsNullOrWhiteSpace(Email) || !IsValidEmail(Email))
            {
                validationErrors += "Invalid email format.\n";
            }

            // Validate Phone
            if (!string.IsNullOrWhiteSpace(Phone) && !IsValidPhone(Phone))
            {
                validationErrors += "Invalid phone number.\n";
            }

            // Validate Address
            if (string.IsNullOrWhiteSpace(Address))
            {
                validationErrors += "Address is required.\n";
            }

            // Validate RegistrationDate
            if (RegistrationDate.HasValue && RegistrationDate > DateTime.Now)
            {
                validationErrors += "Registration date must be in the past or today.\n";
            }

            return string.IsNullOrEmpty(validationErrors);
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPhone(string phone)
        {
            var phonePattern = @"^(03|05|07|08|09)\d{8}$";
            return Regex.IsMatch(phone, phonePattern);
        }
    }
}
