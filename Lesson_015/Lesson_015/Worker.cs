using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_015
{

    internal struct Worker
    {
        private enum EPosition
        {
            Trainer,Doctor,Engineer,Driver,Dancer
        };

        private string lName, name, fName, position;

        private int date;

        private ListWorkers<Worker> listWorkers;

        private string LName
        {
            get { return name; }
            set { lName = value; }
        }

        private string Name
        {
            get { return lName;}
            set { name = value; }
        }

        private string FName
        {
            get { return fName;}
            set { fName = value; }
        }

        private int Date
        {
            get { return date; }
            set { date = value; }
        }

        private string Position
        {
            get { return position; }
            set { position = value; }
        } 

        public void Add()
        { 

            Console.Write($"Фамилия: " );

            FName = Console.ReadLine();
            Console.Write($"Имя: ");

            Name = Console.ReadLine();
            Console.Write($"Отчество: ");

            LName = Console.ReadLine();

            Console.WriteLine($"Выберите профессию: ");

            int count = 1;

            foreach (EPosition pos in (EPosition[])Enum.GetValues(typeof(EPosition)))
            {
                Console.WriteLine($"{count++}. {pos}");
            }

            count = 0;

            int value = int.Parse(Console.ReadLine())-1;

            foreach (EPosition pos in (EPosition[])Enum.GetValues(typeof(EPosition)))
            {

                if (count == value) Position = pos.ToString();

                count++;
            }

            Date = DateTime.Now.Year;
        }

        public void Read()
        {
           
        }

        public void Change()
        {
        }
    }
}
