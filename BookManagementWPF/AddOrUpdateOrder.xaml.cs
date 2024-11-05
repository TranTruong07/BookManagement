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
    /// Interaction logic for AddOrUpdateOrder.xaml
    /// </summary>
    public partial class AddOrUpdateOrder : Window
    {
        private readonly IBookService bookService;
        private readonly ICustomerService customerService;
        private readonly IOrderService orderService;
        public Order CurrentOrder { get; private set; }
        public bool IsEdit { get; private set; }
        public AddOrUpdateOrder()
        {
            InitializeComponent();
            bookService = new BookService(new BookRepository());
            customerService = new CustomerService(new CustomerRepository());
            orderService = new OrderService(new OrderRepository());
            CurrentOrder = new Order();
            IsEdit = false;
        }
        public AddOrUpdateOrder(Order order)
        {
            InitializeComponent();
            bookService = new BookService(new BookRepository());
            customerService = new CustomerService(new CustomerRepository());
            orderService = new OrderService(new OrderRepository());
            CurrentOrder = order;
            cbCustomer.SelectedValue = CurrentOrder.CustomerId;  
            cbBook.SelectedValue = CurrentOrder.BookId;         
            dpBorrowDate.SelectedDate = CurrentOrder.BorrowDate;
            dpReturnDate.SelectedDate = CurrentOrder.ReturnDate;
            IsEdit = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbCustomer.SelectedValue == null || cbBook.SelectedValue == null)
            {
                MessageBox.Show("Please select both a customer and a book.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            CurrentOrder.CustomerId = (int)cbCustomer.SelectedValue;
            CurrentOrder.BookId = (int)cbBook.SelectedValue;
            CurrentOrder.BorrowDate = dpBorrowDate.SelectedDate;
            CurrentOrder.ReturnDate = dpReturnDate.SelectedDate;
            if (!IsEdit)
            {
                int result = orderService.AddOrder(CurrentOrder);
                if (result == 0)
                {
                    MessageBox.Show("Add Order Fail", "Add Order Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                Book? book = bookService.GetBookById(CurrentOrder.BookId);
                Customer? customer = customerService.GetCustomerById(CurrentOrder.CustomerId);
                CurrentOrder.Book = book;
                CurrentOrder.Customer = customer;
                int result = orderService.UpdateOrder(CurrentOrder);
                if (result == 0)
                {
                    MessageBox.Show("Update Order Fail", "Update Order Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomer();
            LoadBook();
        }
        private void LoadCustomer()
        {
            try
            {
                cbCustomer.ItemsSource = customerService.GetAllCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to load Customer " + ex.Message, "Fail");
            }

        }
        private void LoadBook()
        {
            try
            {
                cbBook.ItemsSource = bookService.GetAllBook();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to load Book " + ex.Message, "Fail");
            }

        }
    }
}
