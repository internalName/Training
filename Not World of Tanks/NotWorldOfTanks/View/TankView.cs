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
        private Bitmap tankStartPos = default (Bitmap);

        public TankView()
        {
            wall = Resource_NWoT.wall;
            tank = Resource_NWoT.Tank;
            _tank=new List<Tank>();

        }

        public direction Direction(Tank tank)
        {
            _t = tank;

            return _t.Direction;
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

        public void Draw(PaintEventArgs e, PictureBox pictureBox1)
        {

        }

        public void AddTank(int x,int y)
        {
            _tank.Add(new Tank(x,y));
        }

        public void Move()
        {

        }
    }
}
