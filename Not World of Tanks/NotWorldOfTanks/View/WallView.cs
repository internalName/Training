using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotWorldOfTanks.Model;

namespace NotWorldOfTanks.View
{
    internal sealed class WallView
    {
        private Wall[][] _wall = default(Wall[][]);
        private Point[][] _location = default(Point[][]);

        public WallView(int height,int width)
        {
            _wall = new Wall[height/30][];
            _location=new Point[height/30][];

            for (int i = 0; i < height/30; i++)
            {
            //    _wall[i]=new Wall[];
            }

             BorderConstructor();
        }

        private Wall[][] BorderConstructor()
        {
            

            return _wall;
        }
    }
}
