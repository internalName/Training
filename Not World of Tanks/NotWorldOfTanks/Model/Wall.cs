using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotWorldOfTanks.Model
{
    internal class Wall:GameObject
    {
        private Point _position=default (Point);

        public Point StartPosition=>_position;

        public Wall(int x,int y)
        {
            _position=new Point(x,y);
        }
    }
}
