using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace SaveIsolatedStorageFile
{
    class Program
    {
        static void Main(string[] args)
        {
            IsolatedStorageFile myStorage = IsolatedStorageFile.GetUserStoreForAssembly();

            IsolatedStorageFileStream myStream = new IsolatedStorageFileStream("UserSettings.set", FileMode.Create, myStorage);

            StreamWriter userWriter = new StreamWriter(myStream);
            Console.Write("Запись:");
            userWriter.WriteLine(Console.ReadLine());
            userWriter.Close();

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
