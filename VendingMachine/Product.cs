using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    class Product
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public string ProductType { get; set; }
        public Product()
        {

        }

        public Product(string productName, decimal price, string productType)
        {
            ProductName = productName;
            Price = price;
            ProductType = productType;
        }
    }
}
