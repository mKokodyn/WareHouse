using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.Products
{
    public class Yogurt : DairyProduct
    {
        public override ProductName ProductName { get; } = ProductName.Yogurt;
        public override string TradeMark { get; set; } = string.Empty;
        public override double Price { get; set; }
        public Yogurt(string tradeMark, double price)
        {
            TradeMark = tradeMark;
            Price = price;
        }
    }
}
