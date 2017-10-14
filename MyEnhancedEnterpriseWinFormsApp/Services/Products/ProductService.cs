namespace MyEnterpriseWinFormsApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    using MyEnterpriseWinFormsApp.Models;
    using MyEnterpriseWinFormsApp.Services.Notifications;

    using Windows.ApplicationModel.Payments;

    /// <summary>
    /// Defines a service providing products.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly SemaphoreSlim productSemaphore;

        private readonly List<Product> allProducts;

        private IntPtr handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        public ProductService(IntPtr handle)
        {
            this.handle = handle;
            this.productSemaphore = new SemaphoreSlim(1, 1);
            this.allProducts = new List<Product>();
        }

        /// <summary>
        /// Adds a new product to the system.
        /// </summary>
        /// <param name="product">
        /// The product to add.
        /// </param>
        /// <returns>
        /// Returns an asynchronous operation.
        /// </returns>
        public async Task AddProductAsync(Product product)
        {
            await this.productSemaphore.WaitAsync().ConfigureAwait(false);

            try
            {
                if (product != null)
                {
                    int productId = this.allProducts.Count + 1;
                    product.Id = productId;
                    this.allProducts.Add(product);
                }
            }
            finally
            {
                this.productSemaphore.Release();
            }
        }

        /// <summary>
        /// Adds a collection of new products to the system.
        /// </summary>
        /// <param name="products">
        /// The products to add.
        /// </param>
        /// <returns>
        /// Returns an asynchronous operation.
        /// </returns>
        public async Task AddProductsAsync(IEnumerable<Product> products)
        {
            if (products == null)
            {
                return;
            }

            foreach (Product product in products)
            {
                await this.AddProductAsync(product).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets all of the products in the system.
        /// </summary>
        /// <returns>
        /// Returns the collection of products.
        /// </returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return this.allProducts;
        }

        /// <summary>
        /// Gets a product from the system by the given identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier of the product.
        /// </param>
        /// <returns>
        /// Returns the product if known.
        /// </returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            await this.productSemaphore.WaitAsync().ConfigureAwait(false);

            try
            {
                return this.allProducts.FirstOrDefault(x => x.Id.Equals(id));
            }
            finally
            {
                this.productSemaphore.Release();
            }
        }

        /// <summary>
        /// Updates the given product in the system.
        /// </summary>
        /// <param name="product">
        /// The product to update.
        /// </param>
        /// <returns>
        /// Returns true if the product updated successfully.
        /// </returns>
        public async Task<bool> UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                return false;
            }

            Product existingProduct = await this.GetProductByIdAsync(product.Id).ConfigureAwait(false);

            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;

            return true;
        }

        /// <summary>
        /// Deletes a product in the system by the given identifier.
        /// </summary>
        /// <param name="id">
        /// The identifier of the product.
        /// </param>
        /// <returns>
        /// Returns true if the product deleted successfully.
        /// </returns>
        public async Task<bool> DeleteProductAsync(int id)
        {
            Product existingProduct = await this.GetProductByIdAsync(id).ConfigureAwait(false);
            if (existingProduct == null)
            {
                return false;
            }

            await this.productSemaphore.WaitAsync().ConfigureAwait(false);

            try
            {
                this.allProducts.Remove(existingProduct);
                return true;
            }
            finally
            {
                this.productSemaphore.Release();
            }
        }

        /// <summary>
        /// Clears all of the products in the system.
        /// </summary>
        /// <returns>
        /// Returns an asynchronous operation.
        /// </returns>
        public async Task ClearProductsAsync()
        {
            await this.productSemaphore.WaitAsync().ConfigureAwait(false);

            try
            {
                this.allProducts.Clear();
            }
            finally
            {
                this.productSemaphore.Release();
            }
        }

        [ComImport, Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize([In] IntPtr hwnd);
        }

        public async Task PurchaseProductAsync(Product product)
        {
            if (product != null)
            {
                PaymentMediator mediator = new PaymentMediator();
                ((IInitializeWithWindow)(object)mediator).Initialize(this.handle);

                PaymentDetails paymentDetails = new PaymentDetails(
                                                        new PaymentItem(
                                                            "Total",
                                                            new PaymentCurrencyAmount(
                                                                product.Price.ToString(CultureInfo.CurrentCulture),
                                                                "GBP")),
                                                        new List<PaymentItem>()
                                                            {
                                                                new PaymentItem(
                                                                    product.Name,
                                                                    new PaymentCurrencyAmount(
                                                                        product.Price.ToString(
                                                                            CultureInfo.CurrentCulture),
                                                                        "GBP"))
                                                            })
                                                        {
                                                            ShippingOptions =
                                                                GetShippingOptions()
                                                        };
                List<PaymentMethodData> methods =
                    new List<PaymentMethodData> { new PaymentMethodData(new List<string>() { "basic-card" }) };
                PaymentMerchantInfo merchantInfo = new PaymentMerchantInfo(new Uri("http://www.myenterprise.co.uk"));
                PaymentOptions options = new PaymentOptions()
                                             {
                                                 RequestShipping = true,
                                                 ShippingType = PaymentShippingType.Delivery,
                                                 RequestPayerEmail = PaymentOptionPresence.Optional,
                                                 RequestPayerName = PaymentOptionPresence.Required,
                                                 RequestPayerPhoneNumber = PaymentOptionPresence.None
                                             };

                PaymentRequest paymentRequest = new PaymentRequest(paymentDetails, methods, merchantInfo, options);

                PaymentRequestSubmitResult res = await mediator.SubmitPaymentRequestAsync(paymentRequest);

                if (res.Status == PaymentRequestStatus.Succeeded)
                {
                    await res.Response.CompleteAsync(PaymentRequestCompletionStatus.Succeeded);

                    NotificationService.Current.Show(
                        "Payment Successful",
                        $"The payment for '{product}' was successful!",
                        null,
                        null);
                }
                else
                {
                    NotificationService.Current.Show(
                        "Payment Failed",
                        $"A problem occurred while paying for '{product}'!",
                        null,
                        null);
                }
            }
        }

        private static IReadOnlyList<PaymentShippingOption> GetShippingOptions()
        {
            List<PaymentShippingOption> paymentShippingOptions = new List<PaymentShippingOption>();

            PaymentShippingOption option = new PaymentShippingOption(
                "Free (3-5 business days)",
                new PaymentCurrencyAmount("0.00", "GBP"),
                false,
                "FREE");
            paymentShippingOptions.Add(option);

            option = new PaymentShippingOption("Next day", new PaymentCurrencyAmount("5.99", "GBP"), false, "ONEDAY");
            paymentShippingOptions.Add(option);

            option = new PaymentShippingOption(
                "Priority (by 13:00 next day)",
                new PaymentCurrencyAmount("6.99", "GBP"),
                false,
                "PRIORITY");
            paymentShippingOptions.Add(option);

            return paymentShippingOptions;
        }
    }
}