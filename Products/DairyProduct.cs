using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.Products
{
    public abstract class DairyProduct : IProduct
    {
        public ProductType ProductType { get; } = ProductType.DairyProduct;
        public abstract ProductName ProductName { get; }
        public ulong ProductId { get; set; } = (ulong)DateTime.Now.Ticks;
        public abstract string TradeMark { get; set; }
        public abstract double Price { get; set; }
        public override string ToString()
        {
            return $"Product Type: {ProductType.ToString()}\t Product:[{ProductName.ToString()}]\tBrand [{TradeMark}]\tPrice [{Price}]\tID [{ProductId}]";
        }
    }
}
