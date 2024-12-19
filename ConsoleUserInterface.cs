using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WareHouse.Products;
using WareHouse.ProductsRepository;

namespace WareHouse
{
    public class ConsoleUserInterface
    {
        private WareHouse _wareHouse;
        private ProductsJsonRepository _productsJsonRepository;
        public ConsoleUserInterface(WareHouse wareHouse, ProductsJsonRepository jsonRepository)
        {
            _wareHouse = wareHouse;
            _productsJsonRepository = jsonRepository;
        }
        /// <summary>
        /// Display start menu and general information about products we have
        /// </summary>
        public void DisplayGeneralProductInfo()
        {
            Console.Clear();
            Console.WriteLine("WAREHOUSE OF");
            Console.WriteLine("DAIRY AND MEAT PRODUCTS \n\n");
            Console.WriteLine("Information:\n");

            Console.WriteLine("* Dairy products \t");
            Console.WriteLine($"\n- Milk: {_wareHouse.CountProductsByProductName(ProductName.Milk)}");
            Console.WriteLine($"- Yogurt: {_wareHouse.CountProductsByProductName(ProductName.Yogurt)}");
            Console.WriteLine("\n* Meat products \t");
            Console.WriteLine($"\n- Meat: {_wareHouse.CountProductsByProductName(ProductName.Meat)}");
            Console.WriteLine($"- Sausage: {_wareHouse.CountProductsByProductName(ProductName.Sausage)}");

            Console.WriteLine("\nMENU:\n");

            Console.WriteLine("1. Display specific amount of Products in selected order of Product ID.\n");
            Console.WriteLine("2. Display a list of Products with a price higher than entered.\n");
            Console.WriteLine("3. Display a list of Products amount of which is bigger than entered.\n");
            Console.WriteLine("4. Add Product to the warehouse.\n");
            Console.WriteLine("5. Remove Product from the warehouse by ProductID.\n");

            Console.WriteLine("Choose one of menu item, or type EXIT to save all changes and close the application.\n");
        }

        /// <summary>
        /// Get products in order by product's ID
        /// </summary>
        public void GetProductsInOrder()
        {
            bool isDataEntered = false;
            string? input;
            bool isParsed = false;

            List<ProductName> productNames = new List<ProductName>();
            OrderType orderType = default; //= OrderType.AsItIs;
            int productName;
            int order;
            int amount = default;

            Console.Clear();
            Console.WriteLine("Display specific amount and type of Products in order of descending Product ID.\n");
            Console.WriteLine("Select one, several or all products: ");
            Console.WriteLine("Available product:\n");
            Console.WriteLine("1. Milk ");
            Console.WriteLine("2. Yogurt");
            Console.WriteLine("3. Meat");
            Console.WriteLine("4. Sausage");
            Console.WriteLine("5. All");
            Console.WriteLine("\nIf you entered all products you want to see information about, type Exit:\n");

            while (!isDataEntered)
            {
                input = Console.ReadLine();
                isParsed = Int32.TryParse(input, out productName);



                if (isParsed && productName > 0 && productName < 6)
                {

                    if (isParsed && input == "5")
                    {
                        productNames.Add(ProductName.All);
                        isDataEntered = true;
                        break;
                    }

                    if (productNames.Contains((ProductName)productName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Product you have entered is already in the list.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        productNames.Add((ProductName)productName);
                        Console.WriteLine($"{(ProductName)productName} was added to the list.");
                        if(productNames.Count == 4)
                        {
                            isDataEntered = true;
                            break;
                        }
                    }
                }
                if ((!isParsed && input?.ToLower() != "exit") || (isParsed && (productName > 5 || productName < 1)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Data you have entered is in wrong format.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }


                if (input?.ToLower() == "exit" && productNames.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You did not select any product from the list.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                if (input?.ToLower() == "exit" && productNames.Count != 0)
                {
                    isDataEntered = true;
                }

            }

            isDataEntered = false;

            Console.WriteLine("Select the order's type in which you want to sort the products : ");
            Console.WriteLine("Available order's types:\n");
            Console.WriteLine("1. Descending ");
            Console.WriteLine("2. Ascending");
            Console.WriteLine("3. Do not sort");
            while (!isDataEntered)
            {
                input = Console.ReadLine();
                isParsed = Int32.TryParse(input, out order);
                
                if(isParsed)
                {
                    if (order > 0 && order < 4)
                    {
                        orderType = (OrderType)order;
                        isDataEntered = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You did not select one of the orders's type.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }

                if(!isParsed)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Data you have entered is in wrong format.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }



            }
            isDataEntered= false;

            Console.WriteLine("Enter the amount of products to display:");
            while (!isDataEntered)
            {
                input = Console.ReadLine();
                isParsed = Int32.TryParse(input, out amount);
                if(isParsed)
                {
                    isDataEntered = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Data you have entered is in wrong format. Enter number.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            }

            List<IProduct> products = _wareHouse.GetProductsInOrderByProductsID(productNames, orderType, amount);
            
            if(products.Count > 0)
            {
                Console.WriteLine("Products that fit the condition:\n");
                Print(products);
            }
            else
            {
                Console.WriteLine("There is no any products that fit the condition.");
            }

        }
        /// <summary>
        /// Get products by product's type and product's price
        /// </summary>
        public void GetProductsByTypeAndPrice()
        {
            List<IProduct> products = new List<IProduct>();
            bool isDataEntered = false;
            string? input = string.Empty;

            int productType = 0;
            double price = 0;
            bool isParsable = false;

            Console.Clear();
            Console.WriteLine("Get products by product type and with the price lower than entered:\n");
            Console.WriteLine("Select product type.\n");
            Console.WriteLine("Available product's types:\n");
            Console.WriteLine("1. Dairy products.");
            Console.WriteLine("2. Meat Products.");
           
            while (!isDataEntered)
            {
                input = Console.ReadLine();
                isParsable = Int32.TryParse(input,out productType);
                if (isParsable)
                {
                    if((ProductType)productType == ProductType.DairyProduct 
                            || (ProductType)productType == ProductType.MeatProduct)
                    {
                        Console.WriteLine($"Product type you have chosen is {(ProductType)productType}");
                        isDataEntered = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no such product type. Please enter correct product type.");
                        Console.ForegroundColor= ConsoleColor.Gray;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Data you entered is in wrong format.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            isDataEntered = false;
            Console.Write("Enter the price:");
            while (!isDataEntered)
            {
                input = Console.ReadLine();
                isParsable = double.TryParse(input, out price);
                if (isParsable)
                {
                    if (price > 0)
                    {
                        isDataEntered = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The price can not be less than or equal to zero.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Data you entered is in wrong format.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            }
            products = _wareHouse.GetProductsByTypeAndPrice((ProductType)productType, price);
            
            if (products.Count == 0)
            {
                Console.WriteLine("There is no any products that fit the condition.");
            }
            else
            {
                Console.WriteLine("Products that fit the condition:\n");
                Print(products);
            }
        }

        /// <summary>
        /// Get products amount of which is greater than entered.
        /// </summary>
        public void GetProductsByAmount()
        {
            bool isDataEntered = false;
            string? input = string.Empty;
            bool isParsed = false;

            List<ProductName> productNames = new List<ProductName>();
            //OrderType orderType = default;
            int productName = default;
            int amount = default;

            Console.Clear();
            Console.WriteLine("Display products amount of which is greater than entered.\n");
            Console.WriteLine("Select one, several or all products: ");
            Console.WriteLine("Available products:\n");
            Console.WriteLine("1. Milk ");
            Console.WriteLine("2. Yogurt");
            Console.WriteLine("3. Meat");
            Console.WriteLine("4. Sausage");
            Console.WriteLine("5. All");
            Console.WriteLine("\nIf you entered all products you want to see information about, type Exit:\n");

            while (!isDataEntered)
            {
                input = Console.ReadLine();
                isParsed = Int32.TryParse(input, out productName);

                if (isParsed && productName > 0 && productName < 6)
                {

                    if (isParsed && input == "5")
                    {
                        productNames.Add(ProductName.All);
                        isDataEntered = true;
                        break;
                    }

                    if (productNames.Contains((ProductName)productName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Product you have entered is already in the list.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        productNames.Add((ProductName)productName);
                        Console.WriteLine($"{(ProductName)productName} was added to the list.");
                        if (productNames.Count == 4)
                        {
                            isDataEntered = true;
                            break;
                        }
                    }
                }

                if ((!isParsed && input?.ToLower() != "exit") || (isParsed && (productName > 5 || productName < 1)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Data you have entered is in wrong format.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }


                if (input?.ToLower() == "exit" && productNames.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You did not select any product from the list.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                if (input?.ToLower() == "exit" && productNames.Count != 0)
                {
                    isDataEntered = true;
                }

            }

            isDataEntered = false;
            Console.WriteLine("Enter the quantity to compare with the product's amount:");
            while (!isDataEntered)
            {
                input = Console.ReadLine();
                isParsed = Int32.TryParse(input, out amount);
                if (isParsed)
                {
                    isDataEntered = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Data you have entered is in wrong format. Enter number.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            }

            Dictionary<ProductName, List<IProduct>> productsDictionary = _wareHouse.GetProductsByAmount(productNames, amount);

            Console.WriteLine($"Products, the amount of which is greater than {amount}");
            foreach (var products in productsDictionary)
            {
                Console.WriteLine($"\n{products.Key} products:");
                foreach (var product in products.Value)
                {
                    Console.WriteLine(product);
                }
            }
        }

        /// <summary>
        /// Add product to the warehouse
        /// </summary>
        public void AddProduct()
        {
            bool isDataEntered = false;
            string? input;
            bool isParsed = false;

            int productName = default;
            string name = default!;
            double price = default;

            Console.Clear();
            Console.WriteLine("Adding product to the warehouse:\n");
            Console.WriteLine("Select product from the list to create the Product:\n");
            Console.WriteLine("Available products:\n");
            Console.WriteLine("1. Milk ");
            Console.WriteLine("2. Yogurt");
            Console.WriteLine("3. Meat");
            Console.WriteLine("4. Sausage");

            while (!isDataEntered)
            {
                input = Console.ReadLine();
                isParsed = Int32.TryParse(input, out productName);
                if (isParsed)
                {
                    if (productName > 0 && productName <= 4)
                    {
                        isDataEntered = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("There is no such Product in the list.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Data you have entered is in wrong format.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            isDataEntered = false;
            while (!isDataEntered)
            {
                Console.Write($"Enter the trade mark of the {(ProductName)productName}: ");
                name = Console.ReadLine();

                if (name?.Length > 0)
                {
                    isDataEntered = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Brand of the Product can not be empty!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            isDataEntered = false;
            while (!isDataEntered)
            {
                Console.Write($"Enter the price of the {(ProductName)productName}:");
                input = Console.ReadLine();
                isParsed = double.TryParse(input, out price);
                if (isParsed && price > 0)
                {
                    isDataEntered = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Price of the Product can not be text value, equal or less than zero!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            }

            _wareHouse.AddProduct((ProductName)productName, name ?? string.Empty, price);
            Console.WriteLine("Product was added to the warehouse.");
        }
        /// <summary>
        /// Remove product with specified product's ID the from warehouse
        /// </summary>
        public void RemoveProductByID()
        {
            ulong productID = default;
            string? input = default;
            bool isParsed = false;
            bool isRemoved = false;
            bool isDataEntered = false;

            Console.Clear();
            Console.WriteLine("Remove product from the warehouse.\n");
            Console.WriteLine("Products:\n");
            Print(_wareHouse.GetAllProducts());
            


            while (!isDataEntered)
            {
                Console.WriteLine("\nEnter ProductID of the product you want to remove from warehouse or type CANCEL to exit:");
                input = Console.ReadLine();
                isParsed = ulong.TryParse(input, out productID);
               
                if(input?.ToLower() == "cancel")
                {
                    Console.WriteLine("You did not remove any products from warehouse.");
                    Console.WriteLine();
                    break;
                }

                if (isParsed && productID >= 0)
                {
                    isRemoved = _wareHouse.RemoveProduct(productID);
                    if (isRemoved)
                    {
                        Print(_wareHouse.GetAllProducts());
                        Console.WriteLine($"\nProduct with {productID} ProductID was succsesfully removed from the warehouse.\n");
                        isDataEntered = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Product with ProductID you have entered does not exist in the warehouse.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Data you have entered is in wrong format.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }



        }
        /// <summary>
        /// Print products on the console
        /// </summary>
        /// <param name="products"></param>
        public void Print(List<IProduct> products)
        {
            foreach (IProduct product in products)
            {
                Console.WriteLine(product);
            }
        }
        public void RunApp(string fileName)
        {
            _wareHouse.Products = _productsJsonRepository.LoadProducts(fileName);
            string? input = string.Empty;

            while (input?.ToLower() != "exit")
            {
                DisplayGeneralProductInfo();
                input = Console.ReadLine();

                switch(input)
                {
                    case "1": // Display specific amount of Products in selected order of Product ID
                        GetProductsInOrder();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "2": //Display a list of Products with a price higher than entered
                        GetProductsByTypeAndPrice();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3": //Display a list of Products amount of which is bigger than entered
                        GetProductsByAmount();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4": //Add Product to the warehouse
                        AddProduct();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "5": //Remove Product from the warehouse by ProductID
                        RemoveProductByID();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;

                    case "exit": //Close Application
                        _productsJsonRepository.SaveProducts(fileName, _wareHouse.Products);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("The data you have entered does not correspond to any of the menu item.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

    }
}
