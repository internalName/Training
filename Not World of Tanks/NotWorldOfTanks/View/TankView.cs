using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotWorldOfTanks.View
{
    class TankView
    {
        public Bitmap wall = default(Bitmap);
        public Bitmap tank = default(Bitmap);
        public Point _targetPosition = default(Point);

        //public Point PointPosition
        //{
        //    get => _targetPosition;
        //    set => _targetPosition = value;
        //}

        //public int X
        //{
        //    get => _targetPosition.X;
        //    set => _targetPosition.X = value;
        //}

        //public int Y
        //{
        //    get => _targetPosition.Y;
        //    set => _targetPosition.Y = value;
        //}

        public TankView()
        {
            wall = Resource_NWoT.wall;
            tank = Resource_NWoT.Tank;
            
        }

        public void Draw(PaintEventArgs e, PictureBox pictureBox1)
        {
            Graphics g = e.Graphics;
            g.DrawImage(tank, new Rectangle(_targetPosition.X, _targetPosition.Y, 30, 30));

            for (int height = 0, width = 0;
                (height < pictureBox1.Width) && (width < pictureBox1.Height);
                height += 30, width += 30)
            {
                g.DrawImage(wall, new Rectangle(height, 0, 30, 30));
                g.DrawImage(wall, new Rectangle(0, width, 30, 30));
                g.DrawImage(wall, new Rectangle(height, pictureBox1.Width - 30, 30, 30));
                g.DrawImage(wall, new Rectangle(pictureBox1.Height - 30, width, 30, 30));

            }
        }
    }
}
