

using System;
using System.Diagnostics;
using System.Timers;
using Timer = System.Threading.Timer;

namespace DeleateObjectsPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
                timer.Start();
                new Overflowing();
            timer.Stop();
            
            
            Console.WriteLine($"{timer.ElapsedMilliseconds/100}");
            Console.ReadLine();
        }
    }
}
