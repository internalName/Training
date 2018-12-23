using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotWorldOfTanks.Model;
using Point = System.Drawing.Point;

namespace NotWorldOfTanks.View
{
    class TankView
    {
        public Bitmap wall = default(Bitmap);
        public Bitmap tank = default(Bitmap);
        public List<Tank> _tank = default(List<Tank>);
        private Tank _t;
        private Bitmap tankStartPos = default(Bitmap);

        private Point _startPosition = default(Point);

        public Point StartPosition => _startPosition;

        public TankView(int tankNumber)
        {
            wall = Resource_NWoT.wall;
            tank = Resource_NWoT.Tank;
            _tank = new List<Tank>();
            SetPosition();
        }

        private void SetPosition()
        {
            for (int i = 0; i < _tank.Count; i++)
            {
                
            }
        }

        public direction Direction(Tank tank)
        {
            _t = tank;

            return _t.Direction;
        }

        public Bitmap IfWall(direction dir)
        {
            tankStartPos = Resource_NWoT.Tank;

            if (dir == direction.Down)
            {
                tankStartPos.RotateFlip(RotateFlipType.Rotate180FlipNone);
                return tankStartPos;
            }

            if (dir == direction.Right)
            {
                tankStartPos.RotateFlip(RotateFlipType.Rotate90FlipNone);
                return tankStartPos;
            }

            if (dir == direction.Left)
            {
                tankStartPos.RotateFlip(RotateFlipType.Rotate270FlipNone);
                return tankStartPos;
            }

            if (dir == direction.Up) return tankStartPos;

            return tankStartPos;
        }

        public Bitmap Flip(direction dir)
        {
            tankStartPos = Resource_NWoT.Tank;

            if (dir == direction.Down)
            {
                tankStartPos.RotateFlip(RotateFlipType.Rotate180FlipNone);
                return tankStartPos;
            }

            if (dir == direction.Right)
            {
                tankStartPos.RotateFlip(RotateFlipType.Rotate90FlipNone);
                return tankStartPos;
            }

            if (dir == direction.Left)
            {
                tankStartPos.RotateFlip(RotateFlipType.Rotate270FlipNone);
                return tankStartPos;
            }

            if (dir == direction.Up) return tankStartPos;

            return tankStartPos;
        }

        public void AddTank(int x, int y)
        {
            _tank.Add(new Tank(x, y));
        }

        public void Move(Tank tank)
        {

            //if ((tank.Direction is direction.Up) && tank.Y > 30 &&
            //    !points.Any(n => n.Equals(new Point(_tankView._tank[i].X, _tankView._tank[i].Y - 30))))
            //{
            //    _tankView._tank[i].Y -= 1;
            //    if (teleport)
            //    {
            //        if (dir == 1) break;
            //        else if (dir == 2)
            //        {
            //            _tankView._tank[i].Direction = direction.Left;
            //            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
            //        }
            //        else if (dir == 3)
            //        {
            //            _tankView._tank[i].Direction = direction.Down;
            //            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
            //        }
            //        else if (dir == 4)
            //        {
            //            _tankView._tank[i].Direction = direction.Right;
            //            _tankView.tank = _tankView.Flip(_tankView._tank[i].Direction);
            //        }
            //    }
            //}
        }
    }
}
