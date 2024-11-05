using Repositories.Implement;
using Services;
using Services.Implement;
using System;
using System.Windows;

namespace BookManagementWPF
{
    /// <summary>
    /// Interaction logic for CategoryWPF.xaml
    /// </summary>
    public partial class CategoryWPF : Window
    {
        private readonly ICategoryService categoryService;
        public CategoryWPF()
        {
            InitializeComponent();
            categoryService = new CategoryService(new CategoryRepository());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategory();
        }
        private void LoadCategory()
        {
            try
            {
                dgCategories.ItemsSource = null;
                dgCategories.ItemsSource = categoryService.GetAllCategory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to load Category " + ex.Message, "Fail");
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWPF addCategoryWPF = new AddCategoryWPF();
            if(addCategoryWPF.ShowDialog() == true) 
            {
                dgCategories.ItemsSource = categoryService.GetAllCategory();
            }
        }
    }
}
