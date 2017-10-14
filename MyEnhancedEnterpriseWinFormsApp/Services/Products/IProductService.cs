namespace MyEnterpriseWinFormsApp.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MyEnterpriseWinFormsApp.Models;

    /// <summary>
    /// Defines an interface for a service providing products.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Adds a new product to the system.
        /// </summary>
        /// <param name="product">
        /// The product to add.
        /// </param>
        /// <returns>
        /// Returns an asynchronous operation.
        /// </returns>
        Task AddProductAsync(Product product);

        /// <summary>
        /// Adds a collection of new products to the system.
        /// </summary>
        /// <param name="products">
        /// The products to add.
        /// </param>
        /// <returns>
        /// Returns an asynchronous operation.
        /// </returns>
        Task AddProductsAsync(IEnumerable<Product> products);

        /// <summary>
        /// Gets all of the products in the system.
        /// </summary>
        /// <returns>
        /// Returns the collection of products.
        /// </returns>
        IEnumerable<Product> GetAllProducts();

        /// <summary>
        /// Gets a product from the system by the given identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier of the product.
        /// </param>
        /// <returns>
        /// Returns the product if known.
        /// </returns>
        Task<Product> GetProductByIdAsync(int id);
        
        /// <summary>
        /// Updates the given product in the system.
        /// </summary>
        /// <param name="product">
        /// The product to update.
        /// </param>
        /// <returns>
        /// Returns true if the product updated successfully.
        /// </returns>
        Task<bool> UpdateProductAsync(Product product);

        /// <summary>
        /// Deletes a product in the system by the given identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier of the product.
        /// </param>
        /// <returns>
        /// Returns true if the product deleted successfully.
        /// </returns>
        Task<bool> DeleteProductAsync(int id);

        /// <summary>
        /// Clears all of the products in the system.
        /// </summary>
        /// <returns>
        /// Returns an asynchronous operation.
        /// </returns>
        Task ClearProductsAsync();

        /// <summary>
        /// Prompts the user to purchase the specified product.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// Returns an asynchronous operation.
        /// </returns>
        Task PurchaseProductAsync(Product product);
    }
}