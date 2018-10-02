using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemCollections
{

    internal sealed class Buyer
    {
        private string name;

         internal string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Buyer()
        {

        }

        public Buyer(string name)
        {
            this.name = name;
        }
    }

    enum Product
    {
        Fruit, Auto, Clothes
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region #1
            //var myDyctionary = new Dictionary<Buyer, List<Product>>
            //{
            //    {new Buyer() {Name = "MyMane"}, new List<Product>(){Product.Auto}},
            //    {new Buyer(){Name=$"MyB"},  new List<Product>(){Product.Auto,Product.Clothes,Product.Fruit}  }
            //};


            //foreach (var itemKey in myDyctionary)
            //{
            //    Console.Write($"{itemKey.Key.Name}:");

            //    foreach (var VARIABLE in itemKey.Value)
            //    {
            //        Console.Write($"{VARIABLE} ");
            //    }

            //    Console.WriteLine();
            //}


            #endregion

            #region #2
            //var coll_1=new Dictionary<Int32,float>
            //{
            //    {5,3 }
            //};

            //var coll_2 = new SortedList<int,double>();



            #endregion

            #region #3
            //var myColl=new OrderedDictionary
            //{
            //    {1,"qwe" },{2,"dsfs"},{3,"fdsfds"},{5,"fdg"},{4,"dsfs"}
            //};


            //foreach (var item in myColl.Values)
            //{

            //    foreach (var var in myColl.Values)
            //    {

            //        Console.WriteLine($"{item} ? {var} = {item.Equals(var)}");
            //    }

            //    Console.WriteLine();
            //}


            #endregion

            #region #4
            //var sortedList=new SortedList(new MyComparer())
            //{
            //    {1,"dsc" },{2,"asd"},{3,"sadasd"},{4,"fds"},{5,"aaq"}
            //};

            //foreach (var VARIABLE in sortedList.Values)
            //{
            //    Console.WriteLine(VARIABLE);
            //}


            #endregion



            Console.ReadLine();

        }
    }

    internal sealed class MyComparer : IComparer
    {

        private CaseInsensitiveComparer comparer=new CaseInsensitiveComparer();


        public int Compare(object x, object y)
        {
            return comparer.Compare(y, x);
        }
    }
}
