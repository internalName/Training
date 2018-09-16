using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            var calendar=new TheCalendar();

            var val1 = "hello";
            var val2 = "hello";

            Console.WriteLine();

            

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape) Environment.Exit(0);
            }
            
        }
    }
}
