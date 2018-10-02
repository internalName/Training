using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var @string = String.Empty;
            System.Diagnostics.Stopwatch sw = new Stopwatch();
            sw.Start();

            
           
            for (int i = 0; i < 100; i++)
            {
                @string = $"#{i}";
                System.GC.Collect();
            }
            sw.Stop();

            var sw2=new Stopwatch();
            sw2.Start();
            for (int i = 0; i < 100; i++)
            {
                @string = $"#{i}";
            }
            Console.WriteLine($"1) Excepted: {sw.Elapsed}\n2) Excepted: {sw2.Elapsed}");
            Console.ReadLine();
        }
    }
}
