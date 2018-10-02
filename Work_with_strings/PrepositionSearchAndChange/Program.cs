using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PrepositionSearchAndChange
{
    class Program
    {
        static void Main(string[] args)
        {
            var list_preposition=new string[]{"без",
                "без ведома",
                "безо",
                "благодаря",
                "близ",
                "близко от","связи","бизнеса"};
            var path = @"C:\Users\Дмитрий\Desktop\программирование\book.txt";
            var saveTxt = String.Empty;

            using (var streamReader=new StreamReader(path,Encoding.Default))
            {
                saveTxt = streamReader.ReadToEnd();
            }

            for (int i = 0; i < list_preposition.Length; i++)
            {
                saveTxt=saveTxt.Replace(list_preposition[i],"ГАВ");
            }

            using (var streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(saveTxt);
            }

                Console.ReadLine();
        }
    }
}
