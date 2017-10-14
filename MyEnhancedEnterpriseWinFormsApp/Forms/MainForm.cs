namespace MyEnterpriseWinFormsApp.Forms
{
    using System;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    using MyEnterpriseWinFormsApp.Models;
    using MyEnterpriseWinFormsApp.Services;

    using WinUX.Common;

    /// <summary>
    /// Defines the logic for the main application form.
    /// </summary>
    public partial class MainForm : Form
    {
        private const string ProductStorageFileName = "ProductStorage.xml";

        private DataSet productDataSet;

        private Product selectedProduct;

        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.productService = new ProductService(this.Handle);
        }

        private static Product ToProduct(DataRowView dataRow)
        {
            try
            {
                var product = new Product
                                  {
                                      Id = ParseHelper.SafeParseInt(dataRow.Row.ItemArray[0]),
                                      Name = ParseHelper.SafeParseString(dataRow.Row.ItemArray[1]),
                                      Price = ParseHelper.SafeParseInt(dataRow.Row.ItemArray[2]),
                                      Quantity = ParseHelper.SafeParseInt(dataRow.Row.ItemArray[3])
                                  };
                return product;
            }
            catch (Exception)
            {
                // Ignored
            }

            return null;
        }


        private void OnFormLoaded(object sender, EventArgs e)
        {
            this.InitializeProductsDataSet(this.GetProductStorageFilePath());
            this.UpdateSelectItemState();
        }

        private void InitializeProductsDataSet(string productStorageFilePath)
        {
            this.productDataSet = new DataSet();
            this.productDataSet.ReadXml(productStorageFilePath);

            this.dataGrid.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            this.dataGrid.AutoGenerateColumns = true;
            this.dataGrid.DataSource = this.productDataSet;
            this.dataGrid.DataMember = "Product";
        }

        private string GetProductStorageFilePath()
        {
            string dataFolderPath =
                $@"{
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    }\MyEnterpriseWinFormsApp\";

            string filePath = dataFolderPath + ProductStorageFileName;
            if (File.Exists(filePath))
            {
                return filePath;
            }

            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }

            string installPath = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo installDirectory = Directory.GetParent(installPath);
            File.Copy($@"{installDirectory.FullName}\Services\{ProductStorageFileName}", filePath);

            return filePath;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            this.dataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.editLabel.Visible = true;
            this.saveLabel.Visible = false;

            this.productDataSet.WriteXml(this.GetProductStorageFilePath());
        }

        private void OnEditClicked(object sender, EventArgs e)
        {
            this.editLabel.Visible = false;
            this.saveLabel.Visible = true;

            this.dataGrid.EditMode = DataGridViewEditMode.EditOnKeystroke;
        }

        private void OnSelectedItemChanged(object sender, EventArgs e)
        {
            this.UpdateSelectItemState();
        }

        private void UpdateSelectItemState()
        {
            if (this.dataGrid.SelectedRows.Count <= 0)
            {
                this.UpdateSelectedProduct(null);
                return;
            }

            if (this.dataGrid.SelectedRows[0].DataBoundItem is DataRowView dataRow)
            {
                this.UpdateSelectedProduct(ToProduct(dataRow));
            }
        }

        private void UpdateSelectedProduct(Product product)
        {
            this.selectedProduct = product;
            this.purchaseLabel.Visible = this.editLabel.Visible;
            this.purchaseLabel.Enabled = product != null && product.Quantity > 0;
        }

        private async void OnPurchaseClicked(object sender, EventArgs e)
        {
            if (this.selectedProduct != null)
            {
                await this.productService.PurchaseProductAsync(this.selectedProduct);
            }
        }
    }
}