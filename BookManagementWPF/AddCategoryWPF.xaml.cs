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
    /// Interaction logic for AddCategoryWPF.xaml
    /// </summary>
    public partial class AddCategoryWPF : Window
    {
        public Category CurrentCategory { get; set; }
        private readonly ICategoryService categoryService;
        public AddCategoryWPF()
        {
            InitializeComponent();
            CurrentCategory = new Category();
            categoryService = new CategoryService(new CategoryRepository());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text))
            {
                MessageBox.Show("Category name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            CurrentCategory.CategoryName = txtCategoryName.Text;
            int result = categoryService.AddCategory(CurrentCategory);
            if(result == 0)
            {
                MessageBox.Show("Add Category Fail", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
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
