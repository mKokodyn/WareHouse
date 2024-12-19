using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse.Products;
using WareHouse.Repository;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace WareHouse.ProductsRepository
{
    public class ProductsJsonRepository : IProductsRepository
    {
        private string _filePath;
        public ProductsJsonRepository(string filePath) 
        { 
            _filePath = filePath;
        }
        public List<IProduct> LoadProducts(string filePath)
        {
            List<string> list = new List<string>();
            List<IProduct> products = new List<IProduct>();
            IProduct? product;

            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                string jsonObject = string.Empty;

                string? line = string.Empty;

                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.Trim() != "[")
                    {
                        if (line.Trim() != "]") 
                        {
                            if (line.Trim() != "},")
                            {
                                if (line.Trim() != "}")
                                {
                                    jsonObject += line;
                                }
                            }
                            else if (line.Trim() == "},")
                            {
                                jsonObject += " }";
                                list.Add(jsonObject);
                                jsonObject = string.Empty;
                            }
                            if(line.Trim() == "}")
                            {
                                jsonObject += " }";
                                list.Add(jsonObject);
                                jsonObject = string.Empty;
                            }
                        }
                    }
                }
                foreach(var item in list)
                {
                    int index = item.IndexOf(',', item.IndexOf("ProductName"));
                    string productName = item.Substring(index - 1, 1);
                    product = productName switch 
                    {
                        "1" => JsonSerializer.Deserialize<Milk>(item),
                        "2" => JsonSerializer.Deserialize<Yogurt>(item),
                        "3" => JsonSerializer.Deserialize<Meat>(item),
                        "4" => JsonSerializer.Deserialize<Sausage>(item),

                        _ => throw new NotSupportedException("ProductName is not supported"),
                    };
                    if (product != null)
                    {
                        products.Add(product);
                    }
                }
                
            }
            foreach (var item in products)
            {
                Console.WriteLine(item);
            }
            return products;
        }

        public void SaveProducts(string filePath, List<IProduct> products)
        {
            FileStream fileStream = new FileStream(filePath,FileMode.OpenOrCreate, FileAccess.Write);

            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                };

                string dataToSerialize = JsonSerializer.Serialize(products, options);
                streamWriter.Write(dataToSerialize);
            }
            fileStream.Dispose();
        }
    }
}
