using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.Products
{
    public class Sausage : MeatProduct
    {
        public override ProductName ProductName { get; } = ProductName.Sausage;
        public override string TradeMark { get; set; } = string.Empty;
        public override double Price { get; set; }
        public Sausage(string tradeMark, double price)
        {
            TradeMark = tradeMark;
            Price = price;
        }

    }
}
