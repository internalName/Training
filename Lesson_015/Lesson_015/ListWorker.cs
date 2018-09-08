using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_015
{
    internal class ListWorkers<Worker>
    {
        private List<Worker> list;

        public List<Worker> ReadList { get; set; }

        public int NumberOfWorkers
        {
            get { return list.Count; }
        }

        public ListWorkers()
        {
            list = new List<Worker>();
        }

        public ListWorkers(params Worker[] workers)
        {
            list = new List<Worker>();

            if (workers.Length != 0)
            {

                for (int i = 0; i < workers.Length; i++)
                {
                    list.Add(workers[i]);
                }
            }
        }

        private void SortList()
        {

        }
    }
}
