namespace MyEnterpriseWinFormsApp.Models
{
    /// <summary>
    /// Defines a model representing a product.
    /// </summary>
    public class Product : ModelBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PriceString => this.Price.ToString("C");

        public int Quantity { get; set; }

        public string QuantityString => this.Quantity.ToString("####0");

        public override string ToString()
        {
            return this.Name;
        }
    }
}