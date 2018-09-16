using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Delegate_and_Events
{

    internal delegate int Calc(int a,int b);

    internal class Handler_Collections
    {


        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
