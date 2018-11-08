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
            var char_arr = new char[] {'a',' ','s','2',' ' };
            for (int i = 0; i < char_arr.Length; i++)
            {
                if(char_arr[i]==' ') Console.Write($@"'{char_arr[i]}'"+" ");
                else Console.Write(char_arr[i]+" ");
            }

            char_arr.ToString().Replace(" ", "%20");
            Console.WriteLine();

            for (int i = 0; i < char_arr.Length; i++)
            {
                if (char_arr[i] == ' ') Console.Write($@"'{char_arr[i]}'" + " ");
                else Console.Write(char_arr[i] + " ");
            }
            Console.ReadLine();
        }
    }
}
