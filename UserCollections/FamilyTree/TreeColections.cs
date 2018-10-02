using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree
{
    internal sealed class TreeColections<T> : IEnumerable, IEnumerator
    {
        People[] peoples=new People[4];
        private HashSet<String> family = new HashSet<String>();

        public People this[int index]
        {
            get { return peoples[index]; }
        }

         object IEnumerator.Current=> peoples[position];

        private int position = -1;


         IEnumerator IEnumerable.GetEnumerator()
         {
             return this;
         }

         bool IEnumerator.MoveNext()
        {
            if (position < peoples.Length)
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
    }
}
