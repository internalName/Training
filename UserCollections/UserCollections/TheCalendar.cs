using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCollections
{

   internal sealed class TheCalendar:IEnumerable,IEnumerator
    {

        private static readonly int[] months={1,2,3,4,5,6,7,8,9,10,11,12};

        private static readonly int[] days={30,29,28,31,30,29,28,29,30,31,28,31};

        private int position = -1;

        public int this[int index]
        {
            get { return months[index]; }
        }

        object IEnumerator.Current
        {
            get { return months[position]+" "+days[position]; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        bool IEnumerator.MoveNext()
        {

                if (position < months.Length - 1)
                {
                    position++;
                    return true;
                }
            
            return false;
        }

         void IEnumerator.Reset()
        {
            position = -1;
        }

        public static string SetMonthGetDay(int month)
        {
            if (month > 0 && month < 13) return days[month - 1].ToString();
            return "Месяц за пределами 1-12";
        }

        public static string SetDayGetMonth(int days)
        {
            string result = string.Empty;

            for (int i = 0; i < months.Length; i++)
            {
                if (TheCalendar.days[i] == days) result+=months[i].ToString()+$"\n";
            }

            if (result != string.Empty) return result;
            
            return "Месяц не найден";
        }
    }
}
