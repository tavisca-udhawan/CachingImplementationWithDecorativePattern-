using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductItems items = new ProductItems();
            while(true) { 
            Console.WriteLine("1---Add Product in Cache");
            Console.WriteLine("2---Get Product from Cache");
            Console.WriteLine("3---Exit");
            Console.WriteLine("Enter choice:");
            int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    List<string> item = new List<string>();
                    Console.WriteLine("Enter number of items to add");
                    int number = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter Items:");
                    for (int index = 0; index < number; index++)
                    {
                        string data = Console.ReadLine();
                        item.Add(data);
                    }
                    items.AddItem(item);
                }
                else if (choice == 2)
                {
                    List<string> products = (List<string>)items.GetAvailableItems();
                   foreach (string i in products)
                        Console.WriteLine(i);
                 }
                else if (choice == 3)
                    Environment.Exit(0);
                else
                    Console.WriteLine("Invalid option");

            }
        }
}}
