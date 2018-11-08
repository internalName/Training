using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotWorldOfTanks.Model
{
    internal class Wall
    {
        private Bitmap _picture = Resource_NWoT.wall;
        private Point _position=default (Point);
        private int _numberOfBlocks = default(int);
        private wallDirection _wallDirection=default (wallDirection);

        public Bitmap Picture => Resource_NWoT.wall;
        public Point StartPosition=>_position;
        public int NumberOfBlocks => _numberOfBlocks;
        public wallDirection WallDirection => _wallDirection;

        public Wall()
        {
            
        }
    }
}
