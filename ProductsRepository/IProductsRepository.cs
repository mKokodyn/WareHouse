using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse.Products;

namespace WareHouse.Repository
{
    public interface IProductsRepository
    {
        List<IProduct> LoadProducts(string filePath);
        void SaveProducts(string filepath, List<IProduct> products);
    }
}
