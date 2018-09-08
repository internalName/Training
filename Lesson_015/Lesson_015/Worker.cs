using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_015
{

    internal struct Worker : IWorker
    {
        private string lName, name, fName;
        private WorkPos workPos;
        private int date;

        public Worker(string lName, string name, string fName, WorkPos workPos)
        {
            this.lName = lName;
            this.name = name;
            this.fName = fName;
            this.workPos = workPos;
            date = DateTime.Today.Year;
        }

        public string LName
        {
            get { return name; }
        }

        public string Name
        {
            get { return lName; }
        }

        public string FName
        {
            get { return fName; }
        }

        public WorkPos WorkPos
        {
            get { return workPos; }
        }

        public int Date
        {
            get { return date; }
        }

        public void Read()
        {

        }

        public void Write()
        {
            Console.WriteLine($"Фамилия: {lName}\nИмя: {name}\nДолжность: {workPos}\nДата: {date}");
        }

        public void Change()
        {
        }
    }
}
