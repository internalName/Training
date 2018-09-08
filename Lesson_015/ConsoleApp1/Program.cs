using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            new StringBuilder(Console.ReadLine()).IndexOf('h');
        }
    }

    public static class Test
    {
       public static int IndexOf(this StringBuilder str, char val)
        {
            for (int j = 0; j < str.Length; j++)
                if (str[j] == val)
                    return j;
            return -1;

        }
    }

}
