namespace MyEnterpriseUWPApp.Models
{
    /// <summary>
    /// Defines a model representing a product.
    /// </summary>
    public class Product : ModelBase
    {
        private int quantity;

        private decimal price;

        private string name;

        private int id;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.Set(() => this.Id, ref this.id, value);
            }
        }

        public string Name
        {
            get => this.name;
            set => this.Set(() => this.Name, ref this.name, value);
        }

        public decimal Price
        {
            get => this.price;
            set => this.Set(() => this.Price, ref this.price, value);
        }

        public int Quantity
        {
            get => this.quantity;
            set => this.Set(() => this.Quantity, ref this.quantity, value);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}