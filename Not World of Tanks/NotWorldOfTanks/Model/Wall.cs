using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotWorldOfTanks.Model
{
    internal class Wall
    {
        private Point _startPosition=default (Point);
        private int _numberOfBlocks = default(int);
        private wallDirection _wallDirection=default (wallDirection);

        public Point StartPosition=>_startPosition;
        public int NumberOfBlocks => _numberOfBlocks;
        public wallDirection WallDirection => _wallDirection;

        public Wall()
        {
            
        }
    }
}
