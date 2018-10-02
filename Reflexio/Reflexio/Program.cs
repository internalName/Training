using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexio
{
    delegate void MyDel();

    [My]
    struct TestsAttributeOnStruct
    {

    }
    class Program
    {
        
        public void MyMethod() { }
        
        static void Main(string[] args)
        {
            
        }
    }
    [AttributeUsage(AttributeTargets.Struct)]
    public class MyAttribute:Attribute
    {
        public MyAttribute()
        {
            
        }
    }
}
