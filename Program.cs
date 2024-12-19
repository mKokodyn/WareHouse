using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using WareHouse.Products;
using WareHouse.ProductsRepository;

namespace WareHouse
{
    public class Program
    {
        const string FILEPATH = @"data/products.json";
        static void Main(string[] args)
        {
            
            ProductsJsonRepository jsonRepository = new ProductsJsonRepository(FILEPATH);
            WareHouse wareHouse = new WareHouse();

            ConsoleUserInterface userInterface = new ConsoleUserInterface(wareHouse, jsonRepository);
            
            userInterface.RunApp(FILEPATH);
                        
        }
    }
}
