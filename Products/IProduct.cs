using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.Products
{
    public interface IProduct
    {
        ProductType ProductType { get; }
        ProductName ProductName { get; }
        ulong ProductId { get; set; }
        string TradeMark { get; set; }
        double Price { get; set; }

    }
}
