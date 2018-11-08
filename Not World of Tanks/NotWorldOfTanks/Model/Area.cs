using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotWorldOfTanks.Model
{
   internal sealed class Area
   {
       private int _height = default(int),
                   _width=default (int);

       public int Height => _height;
       public int Width => _width;

       private int DefaultSizeHeight
       {
           get => _height = 500;
       }

       private int DefaultSizeWidth
       {
           get => _width = 500;
       }

        public void ResetSize()
        {
            _width = DefaultSizeWidth;
            _height = DefaultSizeHeight;

        }

        public Area()
       {
           
       }

        public Area(int height,int width)
       {
           _height = height;
           _width = width;
       }
   }
}
