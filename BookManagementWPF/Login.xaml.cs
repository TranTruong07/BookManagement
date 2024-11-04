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
using DataAccess.Models;

namespace BookManagementWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly BookManagementContext context;

        public Login()
        {
            InitializeComponent();
            context = new BookManagementContext();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPass.Password;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Admin? admin = context.Admins.Where(x => x.Username.Equals(username)).FirstOrDefault();

                if (admin != null)
                {
                    if (admin.Password.Equals(password))
                    {
                        txtUser.Text = "OK r đấy";
                        //MainWindow mainWindow = new MainWindow(accountMember, this);
                        //mainWindow.Show();
                        //this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Account not found. Please check your credentials.");
                }
            }
            else
            {
                MessageBox.Show("Please enter Account and Password.");
            }
        }

        

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
