using System;
using System.Collections;

namespace MyWordsCollections
{
    public class DictionaryThree<T>
    {
        private string[] en_arr=new string[8];
        private string[] ru_arr=new string[8];
        private string[] keys=new string[8];
        private string @string = default (string);

        private static int counter = 0;

        public int Count
        {
            get { return en_arr.Length; }
        }

        public string this[int index]
        {
            get { return $"{en_arr[index]}-{ru_arr[index]}"; }
        }

        public DictionaryThree()
        {

        }

       public DictionaryThree(string en_arg,string ru_arg,string key) 
        {
            Add(en_arg,ru_arg,key);
        }

        public void Add(string en_arg, string ru_arg, string key)
        {

            if (counter == keys.Length)
            {
                ru_arr = NewArr(ru_arr);
                en_arr = NewArr(en_arr);
                keys = NewArr(keys);
            }

                this.en_arr[counter] = en_arg;
                this.ru_arr[counter] = ru_arg;
                this.keys[counter] = key;

                counter++;
            

        }

        private string[] NewArr(string[] oldArr)
        {
            var newList = new string[oldArr.Length*2];

            for (int i = 0; i < oldArr.Length; i++)
            {
                newList[i] = oldArr[i];
            }

            return newList;
        }

        public void Print()
        {

            for (int i = 0; i < keys.Length; i++)
            {

                if(keys[i]!=null) Console.WriteLine($"{ru_arr[i]}-{en_arr[i]}");

            }
        }


        public static IEnumerator MethodYield(int[] arr)
        {

            for (int i = 0; i < arr.Length; i++)
            {

                if (arr[i] % 2 != 0) yield return arr[i] * arr[i];

            }
        }
    }
}