using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Implement;
using Services;
using Services.Implement;
using System.Windows;
namespace BookManagementWPF
{
    /// <summary>
    /// Interaction logic for Book.xaml
    /// </summary>
    public partial class BookWPF : Window
    {
        private readonly IBookService bookService;
        private readonly ICategoryService categoryService;
        public BookWPF()
        {
            InitializeComponent();
            bookService = new BookService(new BookRepository());
            categoryService = new CategoryService(new CategoryRepository());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategory();
            LoadBook();
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
        private void LoadBook()
        {
            try
            {
                dgBooks.ItemsSource = null;
                dgBooks.ItemsSource = bookService.GetAllBook();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to load Book " + ex.Message, "Fail");
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            int? publishedYear = string.IsNullOrEmpty(txtPublishedYear.Text) ? (int?)null : int.Parse(txtPublishedYear.Text);
            int? categoryId = cbCategory.SelectedValue != null ? (int?)cbCategory.SelectedValue : null;

            // Filter the book list based on user inputs
            var filteredBooks = bookService.GetAllBook().Where(b =>
                (string.IsNullOrEmpty(title) || b.Title.Contains(title)) &&
                (string.IsNullOrEmpty(author) || b.Author.Contains(author)) &&
                (!publishedYear.HasValue || b.PublishedYear == publishedYear.Value) &&
                (!categoryId.HasValue || b.CategoryId == categoryId.Value)
            ).ToList();

            // Display filtered books in DataGrid
            dgBooks.ItemsSource = filteredBooks;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtPublishedYear.Text = string.Empty;
            cbCategory.SelectedIndex = -1;

            dgBooks.ItemsSource = bookService.GetAllBook();
        }

        private void btnChangeAvailable_Click(object sender, RoutedEventArgs e)
        {
            Book? selectedBook = dgBooks.SelectedItem as Book;

            if (selectedBook == null)
            {
                MessageBox.Show("Please select a book to change its availability.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            selectedBook.IsAvailable = !selectedBook.IsAvailable;
            bookService.ChangeAvailable(selectedBook);

            dgBooks.Items.Refresh();

            MessageBox.Show($"The availability of the book '{selectedBook.Title}' has been changed to {((bool)selectedBook.IsAvailable ? "Available" : "Not Available")}.", "Availability Changed", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditBook addOrEdit = new AddOrEditBook();
            if (addOrEdit.ShowDialog() == true)
            {
                dgBooks.ItemsSource = bookService.GetAllBook();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Book? selectedBook = dgBooks.SelectedItem as Book;

            if (selectedBook == null)
            {
                MessageBox.Show("Please select a book to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AddOrEditBook addOrEdit = new AddOrEditBook(selectedBook);
            if (addOrEdit.ShowDialog() == true)
            {
                dgBooks.Items.Refresh();
            }
        }
    }
}
