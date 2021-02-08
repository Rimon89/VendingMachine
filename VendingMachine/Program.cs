using System;
using System.IO;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationManager appManager = new ApplicationManager();

            appManager.StartApplication();

            Console.ReadKey();
            
        }
    }
}
