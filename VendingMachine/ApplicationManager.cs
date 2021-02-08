using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace VendingMachine
{
    class ApplicationManager
    {

        public ApplicationManager()
        {
            AvailableProducts = new List<Product>();
            CreateProductList();
            BoughtProducts = new List<Product>();
            ProdSum = new List<decimal>();
        }

        private decimal Balance;
        public decimal Price { get; set; }
        public int MoneyInput { get; set; }
        public decimal Sum { get; set; }
        public List<Product> Product { get; set; }
        public List<Product> AvailableProducts { get; set; }
        public List<Product> BoughtProducts { get; set; }
        public List<decimal> ProdSum { get; set; }


        public void StartApplication()
        {
            ShowMainMenu();
            string input = Console.ReadLine();

            while (input.ToLower() != "Q")
            {
                switch (input)
                {
                    case "1":
                        InsertMoney();
                        break;
                    case "2":
                        SubMenu();
                        break;
                    case "3":
                        Receipt();
                        break;
                    default:
                        break;
                }
                ShowMainMenu();
                input = Console.ReadLine();
            }

        }

        private void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("WELCOME!\n\nPress 1 to insert money: ");
            Console.WriteLine("Press 2 to choose product: ");
            Console.WriteLine("Press 3 to purchase your products: ");
            Console.WriteLine("Press 'Q' to quit: ");
            Console.WriteLine();
            Console.WriteLine("Current balance: {0}", Balance);
        }

        public void CreateProductList()
        {

            Product product = new Product("Coca Cola", 15, "Beverage");
            AvailableProducts.Add(product);
            product = new Product("Fanta", 15, "Beverage");
            AvailableProducts.Add(product);
            product = new Product("Sprite", 15, "Beverage");
            AvailableProducts.Add(product);

            product = new Product("Salad", 99, "Food");
            AvailableProducts.Add(product);
            product = new Product("Sushi", 105, "Food");
            AvailableProducts.Add(product);
            product = new Product("Sandwich", 65, "Food");
            AvailableProducts.Add(product);

            product = new Product("Mars", 20, "Snack");
            AvailableProducts.Add(product);
            product = new Product("Snickers", 20, "Snack");
            AvailableProducts.Add(product);
            product = new Product("KitKat", 20, "Snack");
            AvailableProducts.Add(product);

        }

        public void DisplayFoodMenu()
        {
            Console.Clear();
            Console.WriteLine("Current balance: {0}\n", Balance);
            foreach (var i in AvailableProducts)
            {
                Console.WriteLine("{0}. Type: {1}. Product: {2}. Price: {3} kr", AvailableProducts.IndexOf(i) + 1, i.ProductType, i.ProductName, i.Price);
            }
            Console.Write("\nChoose product(1-9) and then enter. Press 'Q' to go back to main menu: ");
        }

        public void CurrencyList()
        {
            Console.Clear();
            Console.WriteLine("Accepted currency:");
            Console.WriteLine("1. 1 kr");
            Console.WriteLine("2. 5 kr");
            Console.WriteLine("3. 10 kr");
            Console.WriteLine("4. 20 kr");
            Console.WriteLine("5. 50 kr");
            Console.WriteLine("6. 100 kr");
            Console.WriteLine("7. 200 kr");
            Console.WriteLine("Q. Main menu");
            Console.WriteLine("\nCurrent balance: {0}\n", Balance);
        }


        public void InsertMoney()
        {
            CurrencyList();
            string input = Console.ReadLine();

            while (input.ToLower() != "q")
            {
                switch (input)
                {
                    case "1":
                        UpdateBalance(1);
                        break;
                    case "2":
                        UpdateBalance(5);
                        break;
                    case "3":
                        UpdateBalance(10);
                        break;
                    case "4":
                        UpdateBalance(20);
                        break;
                    case "5":
                        UpdateBalance(50);
                        break;
                    case "6":
                        UpdateBalance(100);
                        break;
                    case "7":
                        UpdateBalance(200);
                        break;
                    default:
                        break;
                }
                CurrencyList();
                input = Console.ReadLine();
            }
        }

        public void UpdateBalance(int amount)
        {
            Balance += amount;
        }
        public void SubMenu()
        {
            DisplayFoodMenu();
            string myProduct = Console.ReadLine();
            Console.WriteLine("Current balance: {0} kr\n", Balance);

            while (myProduct.ToLower() != "q")
            {
                switch (myProduct)
                {
                    case "1":
                        BuyProduct(0);
                        break;
                    case "2":
                        BuyProduct(1);
                        break;
                    case "3":
                        BuyProduct(2);
                        break;
                    case "4":
                        BuyProduct(3);
                        break;
                    case "5":
                        BuyProduct(4);
                        break;
                    case "6":
                        BuyProduct(5);
                        break;
                    case "7":
                        BuyProduct(6);
                        break;
                    case "8":
                        BuyProduct(7);
                        break;
                    case "9":
                        BuyProduct(8);
                        break;
                    default:
                        break;
                }
                DisplayFoodMenu();
                myProduct = Console.ReadLine();
            }
        }

        public void BuyProduct(int index)
        {

            Product prod = AvailableProducts[index];

            if (prod.Price > Balance)
            {
                Console.Clear();
                Console.WriteLine("You need to insert more money");
                Console.ReadLine();
                InsertMoney();
            }
            else
            {
                Balance = Balance - prod.Price;
                BoughtProducts.Add(prod);
                ProdSum.Add(prod.Price);
            }
        }
        public void Receipt()
        {
            Console.Clear();
            if (Balance >= 0)
            {
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("-------------------------- Receipt ---------------------------\n");

                foreach (var i in BoughtProducts)
                {
                    Console.WriteLine("Type: {0}. Product: {1}. Price: {2} kr.", i.ProductType, i.ProductName, i.Price);
                }

                Sum = ProdSum.Sum();

                Console.WriteLine("\n--------------------------------------------------------------");
                Console.WriteLine("Total: {0} kr", Sum);
                Console.WriteLine("Your change back: {0} kr", Balance);
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("--------------------------------------------------------------");
            }
            //WriteToTextFile();
            Console.ReadLine();

        }

        /*public void WriteToTextFile()
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\Rimon\source\repos\Ny mapp\Kvitto.txt"))
            {
                file.WriteLine("--------------------------------------------------------------");
                file.WriteLine("-------------------------- Receipt ---------------------------\n");
                foreach (var i in BoughtProducts)
                {
                    file.WriteLine("Type: {0}. Product: {1}. Price: {2} kr.", i.ProductType, i.ProductName, i.Price);
                }
                Sum = ProdSum.Sum();

                file.WriteLine("\n--------------------------------------------------------------");
                file.WriteLine("Total: {0} kr", Sum);
                file.WriteLine("Your change back: {0} kr", Balance);
                file.WriteLine("--------------------------------------------------------------");
                file.WriteLine("--------------------------------------------------------------");
            }
        }*/


    }
}

