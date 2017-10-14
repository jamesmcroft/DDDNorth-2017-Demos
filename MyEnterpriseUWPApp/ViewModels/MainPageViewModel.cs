namespace MyEnterpriseUWPApp.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using MyEnterpriseUWPApp.Models;
    using MyEnterpriseUWPApp.Services.Products;

    using Telerik.UI.Xaml.Controls.Grid;

    using WinUX;
    using WinUX.Mvvm;
    using WinUX.Mvvm.Input;

    public class MainPageViewModel : BindableBase
    {
        private bool isPurchaseEnabled;

        private Product selectedProduct;

        private readonly IProductService productService;

        private DataGridUserEditMode currentEditMode = DataGridUserEditMode.None;

        public MainPageViewModel()
        {
            this.productService = App.ProductService;
            this.Products = new ObservableCollection<Product>();

            this.PurchaseItemCommand = new DelegateCommand(async () => await this.PurchaseSelectedItemAsync());

            this.PropertyChanged += this.OnPropertyChanged;
        }

        public bool IsPurchaseEnabled
        {
            get => this.isPurchaseEnabled;
            set => this.Set(() => this.IsPurchaseEnabled, ref this.isPurchaseEnabled, value);
        }

        public ObservableCollection<Product> Products { get; }

        public ICommand PurchaseItemCommand { get; }

        public DataGridUserEditMode CurrentEditMode
        {
            get => this.currentEditMode;
            set => this.Set(() => this.CurrentEditMode, ref this.currentEditMode, value);
        }

        public Product SelectedProduct
        {
            get => this.selectedProduct;
            set => this.Set(() => this.SelectedProduct, ref this.selectedProduct, value);
        }

        public void OnNavigatedTo()
        {
            IEnumerable<Product> products = this.productService.GetAllProducts();

            this.Products.Clear();
            this.Products.AddRange(products);
        }

        private async Task PurchaseSelectedItemAsync()
        {
            if (this.SelectedProduct != null)
            {
                await this.productService.PurchaseProductAsync(this.selectedProduct);
                this.SelectedProduct = null;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(this.SelectedProduct))
            {
                this.IsPurchaseEnabled = this.SelectedProduct != null && this.SelectedProduct.Quantity > 0;
            }
        }
    }
}