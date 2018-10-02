using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree
{
    class People
    {
        private string lName;
        private string name;
        private string sex;
        private int year;

        public int Year
        {
            get { return year; }
            set
            {
                try
                {
                    if (value > 0 && value < DateTime.Now.Year) year = value;
                    else throw new ArgumentException($"Неверно указан год.\nВы указали: {value}");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    value = 1;
                    
                }
                
            }
        }

        public string property_Sex
        {
            get { return sex; }
            internal set
            {
                try
                {
                    if (value.ToUpper().Equals("WOMEN".ToUpper()) || value.ToUpper().Equals("MAN".ToUpper()))
                        sex = value;
                    else throw new ArgumentException("Неверно указан пол");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"{e.Message}\nАвтоматически установлено: WOMEN.");

                    sex = "WOMEN";
                }
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1).ToLower(); }
        }

        public string LName
        {
            get { return lName; }
            set { lName = value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1).ToLower(); }
        }
    }
}
