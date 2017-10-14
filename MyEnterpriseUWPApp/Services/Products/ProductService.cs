namespace MyEnterpriseUWPApp.Services.Products
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MyEnterpriseUWPApp.Models;
    using MyEnterpriseUWPApp.Services.Notifications;

    using Windows.ApplicationModel.Payments;

    /// <summary>
    /// Defines a service providing products.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly SemaphoreSlim productSemaphore;

        private readonly List<Product> allProducts;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        public ProductService()
        {
            this.productSemaphore = new SemaphoreSlim(1, 1);
            this.allProducts = new List<Product>();
        }

        /// <summary>
        /// Initializes the service.
        /// </summary>
        /// <returns>
        /// Returns an asynchronous operation.
        /// </returns>
        public async Task InitializeAsync()
        {
            List<Product> products = new List<Product>()
                               {
                                   new Product
                                       {
                                           Id = 0,
                                           Name = "DDD North Tickets",
                                           Price = (decimal)0.00,
                                           Quantity = 0
                                       },
                                   new Product
                                       {
                                           Id = 1,
                                           Name = "Microsoft Surface Pro - i7 / 1TB / 16GB",
                                           Price = (decimal)2699.00,
                                           Quantity = 101
                                       },
                                   new Product
                                       {
                                           Id = 2,
                                           Name = "Microsoft Lumia 950XL",
                                           Price = (decimal)189.95,
                                           Quantity = 0
                                       },
                                   new Product
                                       {
                                           Id = 3,
                                           Name = "Microsoft Surface Pro - m3 / 128GB / 4GB",
                                           Price = (decimal)799.00,
                                           Quantity = 211
                                       },
                                   new Product
                                       {
                                           Id = 4,
                                           Name = "Microsoft Surface Laptop - i5 / 128GB / 4GB",
                                           Price = (decimal)979.00,
                                           Quantity = 51
                                       },
                                   new Product
                                       {
                                           Id = 5,
                                           Name = "Microsoft Surface Book - i5 / 128GB / 8GB",
                                           Price = (decimal)1199.00,
                                           Quantity = 23
                                       },
                                   new Product
                                       {
                                           Id = 6,
                                           Name = "HP Elite x3",
                                           Price = (decimal)689.00,
                                           Quantity = 0
                                       },
                                   new Product
                                       {
                                           Id = 7,
                                           Name = "Microsoft Surface Studio - i5 / 1TB / 8GB",
                                           Price = (decimal)2999.00,
                                           Quantity = 23
                                       },
                                   new Product
                                       {
                                           Id = 8,
                                           Name = "Alcatel IDOL 4 Pro",
                                           Price = (decimal)419.99,
                                           Quantity = 0
                                       },
                                   new Product
                                       {
                                           Id = 9,
                                           Name = "Xbox One X - 1TB",
                                           Price = (decimal)449.99,
                                           Quantity = 11
                                       },
                                   new Product
                                       {
                                           Id = 10,
                                           Name = "Xbox One S - 1TB",
                                           Price = (decimal)299.99,
                                           Quantity = 99
                                       },
                                   new Product
                                       {
                                           Id = 11,
                                           Name = "Lenovo Yoga 710",
                                           Price = (decimal)799.99,
                                           Quantity = 0
                                       },
                               };

            await this.AddProductsAsync(products);
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

        /// <summary>
        /// Prompts the user to purchase the specified product.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// Returns an asynchronous operation.
        /// </returns>
        public async Task PurchaseProductAsync(Product product)
        {
            if (product != null)
            {
                PaymentMediator mediator = new PaymentMediator();
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