using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLProjects
{   
    class Program
    {
        static void Main(string[] args)
        {   
         var XMLwriter=new XmlTextWriter("TelephoneBook.xml",Encoding.Default)
         {
             Formatting = Formatting.Indented,
             IndentChar = '\t',
             Indentation = 1,
             QuoteChar = '\''
         };
            XMLwriter.IndentChar = ' ';
            XMLwriter.Indentation = 4;
            XMLwriter.WriteStartDocument();
            XMLwriter.WriteStartElement("MyContacts");
            XMLwriter.WriteStartElement("Contact");
            XMLwriter.WriteStartAttribute("TelephoneNumber");
            XMLwriter.WriteString(" + 79156054047");
            XMLwriter.WriteEndAttribute();
            XMLwriter.WriteString("Dima");
            XMLwriter.WriteEndElement();
            XMLwriter.WriteEndElement();
            XMLwriter.Close();

            Console.ReadLine();
        }
    }
}
