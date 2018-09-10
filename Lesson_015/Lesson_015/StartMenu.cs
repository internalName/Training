using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_015
{
    internal sealed class StartMenu
    {
        private Worker worker;
        private int check;
        private ListWorkers<Worker> listWorkers;

        public StartMenu()
        {
            listWorkers=new ListWorkers<Worker>();

            Presentation();
        }

        public int Check
        {
            get { return check; }
            set
            {
                if (value <= 3 && value >= 1) check = value;
                else throw new Exception("Ошибка ввода аргумента.");
            }
        }

        private void Presentation()
        {

            do
            {

                try
                {
                    Console.Write($"1) Добавить сотрудника\n2) Вывести список сотрудников\n3) Выход\n Введите число: ");
                    Check = int.Parse(Console.ReadLine());

                    if(check==3) Environment.Exit(0);

                    break;
                }

                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}\nНажмите Enter, чтобы попробовать ещё раз.\nНажмите 'q' для выхода.");

                    if (Console.ReadKey().Key == ConsoleKey.Q) Environment.Exit(0);

                }

            } while (true);

            worker=new Worker();

            if (check == 1)
            {
                worker.Add();
                listWorkers.Add(worker);
            }
          
            else if (check == 2)
            {
                worker.Read();
            }
            
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
