using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.Products
{
    public class ProductsFactory
    {
        public static IProduct? CreateProduct(ProductName productName, string tradeMark, double price)
        {
            IProduct? product = null;
            switch (productName)
            {
                case ProductName.Milk:
                    product = new Milk(tradeMark, price);
                    return product;

                case ProductName.Yogurt:
                    product = new Yogurt(tradeMark, price);
                    return product;

                case ProductName.Meat:
                    product = new Meat(tradeMark, price);
                    return product;

                case ProductName.Sausage:
                    product = new Sausage(tradeMark, price);
                    return product;

                default:
                    throw new NotSupportedException("Product name is not supported.");
            }
        }
    }
}
