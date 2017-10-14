namespace MyEnterpriseWinFormsApp.Forms
{
    using System;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the logic for the main application form.
    /// </summary>
    public partial class MainForm : Form
    {
        private const string ProductStorageFileName = "ProductStorage.xml";

        private DataSet productDataSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        private void OnFormLoaded(object sender, EventArgs e)
        {
            this.InitializeProductsDataSet(this.GetProductStorageFilePath());
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
    }
}