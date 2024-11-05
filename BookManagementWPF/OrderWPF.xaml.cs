using DataAccess.Models;
using Repositories.Implement;
using Services;
using Services.Implement;
using System.Windows;


namespace BookManagementWPF
{
    /// <summary>
    /// Interaction logic for OrderWPF.xaml
    /// </summary>
    public partial class OrderWPF : Window
    {
        private readonly IOrderService orderService;
        public OrderWPF()
        {
            InitializeComponent();
            orderService = new OrderService(new OrderRepository());
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string customerFullName = txtCustomerFullName.Text.Trim();
            string bookTitle = txtBookTitle.Text.Trim();


            var filteredOrders = orderService.GetAllOrder().Where(order =>
                (string.IsNullOrEmpty(customerFullName) || order.Customer.FullName.Contains(customerFullName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(bookTitle) || order.Book.Title.Contains(bookTitle, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            dgOrders.ItemsSource = filteredOrders;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrder();
        }
        private void LoadOrder()
        {
            try
            {
                dgOrders.ItemsSource = null;
                dgOrders.ItemsSource = orderService.GetAllOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to load Book " + ex.Message, "Fail");
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtCustomerFullName.Clear();
            txtBookTitle.Clear();
            dgOrders.ItemsSource = orderService.GetAllOrder();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateOrder addOrEdit = new AddOrUpdateOrder();
            if (addOrEdit.ShowDialog() == true)
            {
                dgOrders.ItemsSource = orderService.GetAllOrder();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Order? selectOrder = dgOrders.SelectedItem as Order;
            if (selectOrder == null)
            {
                MessageBox.Show("Please select a Order to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AddOrUpdateOrder addOrEdit = new AddOrUpdateOrder(selectOrder);
            if (addOrEdit.ShowDialog() == true)
            {
                dgOrders.Items.Refresh();
            }
        }
    }
}
