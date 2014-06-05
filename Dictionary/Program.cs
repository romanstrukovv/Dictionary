using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            MyDictionary<string> dic1 = new MyDictionary<string>();

            dic1.beginOfFile();
            string str; int n = 0;

            Console.WriteLine("\nThe capacity is: " + dic1.Capacity);
            while (n != 999)
            {               
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n\nPlease, enter the key: ");
                n = int.Parse(Console.ReadLine());
                                
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("\nPlease, enter the value: ");
                str = Console.ReadLine();

                if (n != 999)
                dic1.Add(new NodeDic<string>(n, str));

                Console.Write("The amount of elemets is: " + dic1.Count);
            }

            dic1.endOfFile();
            int m = 0;

            Console.WriteLine();
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;                
                Console.Write("\nPlease, enter a key of the element you want to get:");
                m = int.Parse(Console.ReadLine());
                if (m != 999)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("\nThe value is: {0}", dic1.Find(m));
                }
            } while (m != 999);

            Console.Write("\nPlease, enter a key for removing: ");
            dic1.Remove(int.Parse(Console.ReadLine()));

            do
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nPlease, enter a key of the element you want to get:");
                m = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nThe value is: {0}", dic1.Find(m));
            } while (m != 999);

            Console.ReadKey();
        }
    }
}
