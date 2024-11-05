using Microsoft.VisualBasic.Logging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookManagementWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Login login;
        public MainWindow()
        {
            InitializeComponent();
            //this.login = login;
        }
        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //login.Close();
        }
        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
        }
        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            BookWPF book = new BookWPF();
            book.ShowDialog();
        }
        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            //FlightWPF flightWPF = new FlightWPF(acc);
            //flightWPF.ShowDialog();
        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            OrderWPF order = new OrderWPF();
            order.ShowDialog();
        }

        private void ListViewItem_Selected_4(object sender, RoutedEventArgs e)
        {

            CustomerWPF customer = new CustomerWPF();
            customer.ShowDialog();
        }
        private void ListViewItem_Selected_6(object sender, RoutedEventArgs e)
        {
            this.Close();
            //login.txtPass.Password = "";
            //login.Show();
        }
    }
}