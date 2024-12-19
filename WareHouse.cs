using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WareHouse.Products;
using WareHouse.ProductsRepository;

namespace WareHouse
{
    public class WareHouse
    {
        public List<IProduct> Products { get; set; } = new List<IProduct>();
        public WareHouse()
        {
            
        }

        /// <summary>
        /// Return all products in warehouse
        /// </summary>
        /// <returns></returns>
        public List<IProduct> GetAllProducts()
        {
            return Products;
        }

        //Get products in ascending or descending order depending on the condition
        public List<IProduct> GetProductsInOrderByProductsID(List<ProductName> productNames, OrderType orderType, int amount)
        {
            List<IProduct> sourceProducts = GetProductsByProductName(productNames);

            switch (orderType)
            {
                case OrderType.Descending:

                    return sourceProducts.OrderByDescending(p => p.ProductId).Take(amount).ToList();

                case OrderType.Ascending:

                    return sourceProducts.OrderBy(p => p.ProductId).Take(amount).ToList();

                default:
                    return sourceProducts.Take(amount).ToList();
            }
        }

        //GetProductsByProductType method select specific products by the product's type.
        //As a parameter we use a list of Product Type.
        private List<IProduct> GetProductsByProductName(List<ProductName> productNames)
        {
            if (productNames.Contains(ProductName.All))
            {
                return Products;
            }

            List<IProduct> resultProducts = new List<IProduct>();

            foreach (var productName in productNames)
            {
                resultProducts.AddRange(Products
                .Where(prod => prod.ProductName == productName));
            }

            return resultProducts;
        }
        /// <summary>
        /// Get products by product's type and product's price
        /// </summary>
        /// <param name="productType">Product's type</param>
        /// <param name="price">Product's price</param>
        /// <returns></returns>
        public List<IProduct> GetProductsByTypeAndPrice(ProductType productType, double price)
        {
            List<IProduct> productsByType = Products
                    .Where(p => p.ProductType == productType && p.Price < price)
                        .ToList();
            return productsByType;
        }

        /// <summary>
        /// Gets data about products whose amount exceeds the specified number
        /// </summary>
        /// <param name="productTypes">Types of products we are searching in</param>
        /// <param name="amount">Number of products to compare</param>
        /// <returns>Returns a Dictionary with ProductType as a key and a list of specific products as a value</returns>
        public Dictionary<ProductName, List<IProduct>> GetProductsByAmount(List<ProductName> productNames, int amount)
        {
            Dictionary<ProductName, List<IProduct>> resultProductsDictionary = new Dictionary<ProductName, List<IProduct>>();

            var tempProductsList = Products.GroupBy(p => p.ProductName, (key, products) =>
            new
            {
                key,
                count = products.Count(),
                groupedProducts = products
            }).ToList();

            if (productNames.Contains(ProductName.All))
            {
                foreach (var item in tempProductsList)
                {
                    if (item.count >= amount)
                    {
                        resultProductsDictionary.Add((ProductName)item.key, item.groupedProducts.ToList());
                    }

                }
            }
            else
            {
                foreach (var item in tempProductsList)
                {
                    if (item.count >= amount && productNames.Contains(item.key))
                    {
                        resultProductsDictionary.Add((ProductName)item.key, item.groupedProducts.ToList());
                    }

                }
            }

            return resultProductsDictionary;
        }

        /// <summary>
        /// Count the amount of products by product name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public int CountProductsByProductName(ProductName productName)
        {
            return Products.Where(p => p.ProductName == productName).Count();
        }

        /// <summary>
        /// Add product to the warehouse
        /// </summary>
        /// <param name="productName">Name of the product we want to add</param>
        /// <param name="tradeMark">Trade mark of the product</param>
        /// <param name="price">Price of the product</param>
        public void AddProduct(ProductName productName, string tradeMark, double price)
        {
            IProduct? product = ProductsFactory.CreateProduct(productName, tradeMark, price);

            if (product != null)
            {
                Products.Add(product);
            }
        }

        /// <summary>
        /// Remove product from the warehouse
        /// </summary>
        /// <param name="productID">ID of the product</param>
        /// <returns></returns>
        public bool RemoveProduct(ulong productID)
        {
            bool isRemoved = false;
            foreach (var product in Products)
            {
                if (product.ProductId == productID)
                {
                    Products.Remove(product);
                    isRemoved = true;
                    break;
                }
            }
            return isRemoved;
        }


    }
}
