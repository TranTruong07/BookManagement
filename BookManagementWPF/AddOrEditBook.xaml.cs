using DataAccess.Models;
using Repositories.Implement;
using Services;
using Services.Implement;
using System.Windows;



namespace BookManagementWPF
{
    /// <summary>
    /// Interaction logic for AddOrEditBook.xaml
    /// </summary>
    public partial class AddOrEditBook : Window
    {
        public Book CurrentBook { get; set; }
        public bool IsEditMode { get; set; }
        private readonly ICategoryService categoryService;
        private readonly IBookService bookService;
        public AddOrEditBook()
        {
            InitializeComponent();
            CurrentBook = new Book();
            IsEditMode = false;
            categoryService = new CategoryService(new CategoryRepository());
            bookService = new BookService(new BookRepository());

        }
        

        public AddOrEditBook(Book bookToEdit)
        {
            InitializeComponent();
            CurrentBook = bookToEdit; 
            IsEditMode = true;
            categoryService = new CategoryService(new CategoryRepository());
            bookService = new BookService(new BookRepository());

            txtTitle.Text = CurrentBook.Title;
            txtAuthor.Text = CurrentBook.Author;
            txtPublishedYear.Text = CurrentBook.PublishedYear.ToString();
            cbCategory.SelectedValue = CurrentBook.CategoryId;

            chkIsAvailable.IsChecked = CurrentBook.IsAvailable ?? false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategory();
        }
        private void LoadCategory()
        {
            try
            {
                cbCategory.ItemsSource = null;
                cbCategory.ItemsSource = categoryService.GetAllCategory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to load Category " + ex.Message, "Fail");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtAuthor.Text))
            {
                MessageBox.Show("Please fill in the Title and Author fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CurrentBook.Title = txtTitle.Text;
            CurrentBook.Author = txtAuthor.Text;
            CurrentBook.PublishedYear = int.Parse(txtPublishedYear.Text);
            CurrentBook.CategoryId = (int)cbCategory.SelectedValue;
            CurrentBook.IsAvailable = chkIsAvailable.IsChecked;
            int currentYear = DateTime.Now.Year;
            if(CurrentBook.PublishedYear > currentYear)
            {
                MessageBox.Show("Please fill in the PublishedYear <= Current Year.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!IsEditMode)
            {
                int result = bookService.AddBook(CurrentBook);
                if(result == 0)
                {
                    MessageBox.Show("Add Book Fail", "Add Book Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                int result = bookService.UpdateBook(CurrentBook);
                if (result == 0)
                {
                    MessageBox.Show("Update Book Fail", "Update Book Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            this.DialogResult = true; 
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
