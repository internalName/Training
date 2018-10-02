using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            #region #1
            //Directory.CreateDirectory(@"D:\hello");

            //for (int i = 0; i < 100; i++)
            //{
            //    Directory.CreateDirectory($@"D:\hello\Folder_{i}");
            //}

            //foreach (var VARIABLE in new FileInfo(@"D:\hello\").Directory.EnumerateDirectories())
            //{
            //    Directory.Delete(VARIABLE.FullName);
            //}


            #endregion

            #region #2
            //Directory.CreateDirectory(@"D:\hello");

            //var writer = new StreamWriter($@"D:\hello\file.txt");
            //writer.Write("Hello");

            //writer.Dispose();

            //var reader = new StreamReader(@"D:\hello\file.txt");
            //Console.WriteLine(reader.ReadLine());

            //reader.Close();


            #endregion

            var drive=DriveInfo.GetDrives();

            foreach (var VARIABLE in drive)
            {
                Console.WriteLine(VARIABLE);
            }
            

            Console.ReadLine();
        }
    }
}
