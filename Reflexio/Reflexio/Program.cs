using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AccessLevelAttribute;

namespace Reflexio
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            var xmlFormat = new XmlSerializer(typeof(Employes[]));

            using (var fs = new FileStream(@"C:\Users\Дмитрий\Desktop\программирование\Проекты\GitHub\ITVDN\Training_Essential\Training\Attribute's\AccessLevelAttribute\bin\Debug\user.xml", FileMode.OpenOrCreate))
            {
                Employes[] deserilizePeople = (Employes[])xmlFormat.Deserialize(fs);

                foreach (Employes p in deserilizePeople)
                {
                    Console.WriteLine($"Имя: {p.Name}\nФамилия: {p.LName}\nТип: {p.GetType()}{Environment.NewLine}");
                }
            }

            Console.ReadLine();
        }
    }
}
