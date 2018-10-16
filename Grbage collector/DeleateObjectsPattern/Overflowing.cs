using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleateObjectsPattern
{

    internal sealed class Overflowing:IDisposable
    {
        private byte[] _overflowArray = new Byte[1000000000];
        private bool _disposed = false;

        ~Overflowing()
        {
            //GC.Collect();
            for (int i = 0; i < _overflowArray.Length; i++)
            {
                if (i == 0) _overflowArray[i] = 1;
                unchecked
                {
                    _overflowArray[i] = (byte)i;
                }
            }
        }

        private void CleanUp(bool clean)
        {
            
        }

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }
    }
}
