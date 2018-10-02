using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;


namespace GetInformationXML
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            FileStream stream = new FileStream("TelephoneBook.xml", FileMode.Open);

            XmlTextReader xmlReader = new XmlTextReader(stream);

            while (xmlReader.Read())
            {
                if (xmlReader.HasAttributes)
                {
                    if (xmlReader.Name.Equals("Contact"))
                    {
                        Console.WriteLine("<{0}>", xmlReader.GetAttribute("TelephoneNumber"));
                    }
                }
            }
            
            xmlReader.Close();
            

            Console.ReadLine();
        }
    }
}
