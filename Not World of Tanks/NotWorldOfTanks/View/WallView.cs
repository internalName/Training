using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotWorldOfTanks.Model;
using Point = NotWorldOfTanks.Model.Point;

namespace NotWorldOfTanks.View
{
    internal sealed class WallView
    {
        private List<Wall> _fieldLocation = default(List<Wall>);

        public Bitmap WallImage => Resource_NWoT.wall;

        private int Height { get; set; }

        private int Width { get; set; }

        public List<Wall> FieldLocation => _fieldLocation;

        public WallView(int height,int width)
        {
            _fieldLocation=new List<Wall>();

            Height = height;
            Width = width;

            SetWall();
        }

        private void SetWall()
        {
            SetBorderWall();
            SetRandomWall();
        }

        private void SetBorderWall()
        {
            for (int i = 0,j=0; (i < Math.Max(Height,Width)); i+=30,j+=30)
            {
                if (i < Width)
                {
                    _fieldLocation.Add(new Wall(i,0));
                    _fieldLocation.Add(new Wall(i,Height-30));
                }

                if (j < Height)
                {
                    _fieldLocation.Add(new Wall(0, j));
                    _fieldLocation.Add(new Wall(Width-30, j));
                }
            }

        }

        private void SetRandomWall()
        {
            Random r_h = new Random();

            int count = Width / 10 - 10;
            int a = 0, b = 0;
            for (int i = 0; i < count; i++)
            {
                a = r_h.Next(30, Height - 30) / 30 * 30;
                b = r_h.Next(30, Width - 30) / 30 * 30;

                if (!_fieldLocation.Any(n => new Point(a, b).Equals(n)))
                {
                    _fieldLocation.Add(new Wall(a, b));
                }
                else i -= 1;
            }
        }
    }
}
