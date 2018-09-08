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
        private int check;

        public StartMenu()
        {
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
                    break;
                }

                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}\nНажмите Enter, чтобы попробовать ещё раз.\nНажмите 'q' для выхода.");

                    if (Console.ReadKey().Key == ConsoleKey.Q) Environment.Exit(0);

                }

            } while (true);

            switch (check)
            {
                case 1:
                    Console.WriteLine("its 1");
                    break;
                case 2:
                    Console.WriteLine("its 2");
                    break;
                case 3:
                    Console.WriteLine("its 3");
                    break;
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
