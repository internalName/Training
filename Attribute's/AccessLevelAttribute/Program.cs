using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace AccessLevelAttribute
{

    class Program
    {
        static void Main()
        {

            var manager=new Manager{Name = "managerSasha",LName = "lname1"};
            var director=new Director{Name = "directorPety", LName = "lname2" };
            var programmer=new Programmer{Name = "programmerDenis", LName = "lname3" };

            Employes[] employes =
                {manager, director,programmer};

            #region #1
            //foreach (var variable in employes)
            //{
            //    Console.WriteLine($"{variable.AccessLevelAttribute.AccessLevel}-{variable.Message()}");
            //}
            #endregion

            var xmlFormat = new XmlSerializer(typeof(Employes[]));
            //var binFormat = new BinaryFormatter();

            using (var fStream = new FileStream("user.xml",
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // binFormat.Serialize(fStream, employes);  Сериализация в двоичном формате

                xmlFormat.Serialize(fStream, employes);
            }

            Console.WriteLine($"Succsesful");

            //using (var fs = new FileStream("user.dat", FileMode.OpenOrCreate))
            //{
            //    Employes[] deserilizePeople = (Employes[])binFormat.Deserialize(fs);

            //    foreach (Employes p in deserilizePeople)
            //    {
            //        Console.WriteLine($"Имя: {p.GetType()}");
            //    }
            //}          Десериализация
            Console.ReadLine();
        }

    }
}
