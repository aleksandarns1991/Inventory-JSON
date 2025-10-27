using Inventar.DataAccess;
using Inventar.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Inventar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; } = new();

        private Product? selectedProduct;

        public Product? SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                NotifyPropertyChanged(nameof(SelectedProduct));
            }
        }

        private string? searchText = string.Empty; 

        public string? SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                NotifyPropertyChanged(nameof(SearchText));
                NotifyPropertyChanged(nameof(FilteredProducts));
            }
        }

        public List<Product> FilteredProducts
        {
            get
            {
                var output = Products.Where(p => p.Title!.ToLowerInvariant().StartsWith(SearchText!.ToLowerInvariant())).ToList();
                return output;
            }
        }

        public decimal Total
        {
            get
            {
                var output = soldItems.Sum(s => s.Price);
                return output;
            }
        }

        private readonly ObservableCollection<SoldItem> soldItems = new(); 

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            LoadData();
            NotifyFilteredProducts();
            NotifyTotalForSoldItems();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Serializer.Serialize<Product>(Products);
            Serializer.Serialize<SoldItem>(soldItems);
        }

        private void LoadData()
        {
            Serializer.Deserialize<Product>(Products);
            Serializer.Deserialize<SoldItem>(soldItems);
        }

        private void NotifyFilteredProducts()
        {
            Products.CollectionChanged += (s, e) => NotifyPropertyChanged(nameof(FilteredProducts));
        }

        private void NotifyTotalForSoldItems()
        {
            soldItems.CollectionChanged += (s, e) => NotifyPropertyChanged(nameof(Total));
        }

        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(string? arg)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(arg));   
        }

        #endregion 

        private void btnCleanSearchField_Click(object sender, RoutedEventArgs e)
        {
            SearchText = string.Empty;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Products.Add(new Product());
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Products.Remove(SelectedProduct!);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to remove all articles?","Warning",
                                         MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Products.Clear();
            }
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            soldItems.Add(new SoldItem(SelectedProduct!.ID,SelectedProduct!.Price));
            SelectedProduct.Quantity--;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var item = soldItems.FirstOrDefault(i => i.ProductID == SelectedProduct!.ID)!;

            if (item != null)
            {
                soldItems?.Remove(item);
                SelectedProduct!.Quantity++;
            }
        }
    }
}