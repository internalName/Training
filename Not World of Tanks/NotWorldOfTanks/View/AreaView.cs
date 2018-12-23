using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotWorldOfTanks.Model;
using Point = NotWorldOfTanks.Model.Point;

namespace NotWorldOfTanks.View
{
    public sealed class AreaView
   {
       private Area _area = default(Area);
       private WallView _wallView=default (WallView);
       private TankView _tankView = default(TankView);
       private int _tankNumber = default(int);

       public int TankNumber
       {
           get { return _tankNumber; }
            set
            {
                if (value < 1 || value > Math.Max(AreaSize.Width, AreaSize.Height) / 100) _tankNumber = 5;
                else _tankNumber = value;
            }
       }

       public Size AreaSize => new Size(_area.Height,_area.Width);

        public AreaView(Size size, int numOfTanks, int numOfBonuses, int speed)
        {
            _area=new Area(size.Height,size.Width);
            _wallView=new WallView(size.Height,size.Width);
            TankNumber = numOfTanks;
        }

       public Size SetScale(Size formSize,Size gameArea)
       {

           if (_area.Height > formSize.Height - 100 ||
               _area.Width > formSize.Width - 100)
           {
               ResetSize();
           }
           if (_area.Height < 210 || _area.Width < 210) gameArea = new Size(500, 500);

            if ((Math.Ceiling((double)gameArea.Height / 30) > gameArea.Height / 30)||(Math.Ceiling((double)gameArea.Width / 30) > gameArea.Width / 30))
           {
               _area=new Area(gameArea.Height/30*30, gameArea.Width/30*30);
               return AreaSize;
           }

           _area = new Area(gameArea.Height, gameArea.Width);

            return AreaSize;
       }

       public void SetWall()
       {
           SetBorderWall();
           SetRandomWall();
       }

       private void SetBorderWall()
       {
           for (int i = 0, j = 0; (i < Math.Max(_area.Height, _area.Width)); i += 30, j += 30)
           {
               if (i < _area.Width)
               {
                   _fieldLocation.Add(new Wall(i, 0));
                   _fieldLocation.Add(new Wall(i, _area.Height - 30));
               }

               if (j < _area.Height)
               {
                   _fieldLocation.Add(new Wall(0, j));
                   _fieldLocation.Add(new Wall(_area.Width - 30, j));
               }
           }

       }

       private void SetRandomWall()
       {
           Random r_h = new Random();

           int count = _area.Width / 10 - 10;
           int a = 0, b = 0;
           for (int i = 0; i < count; i++)
           {
               a = r_h.Next(30, _area.Height - 30) / 30 * 30;
               b = r_h.Next(30, _area.Width - 30) / 30 * 30;

               if (!_fieldLocation.Any(n => new Point(a, b).Equals(n)))
               {
                   _fieldLocation.Add(new Wall(a, b));
               }
               else i -= 1;
           }
       }

        public void ResetSize()
       {
           _area.ResetSize();
       }

       public int LocationArea(int clientSize, int areaSize) => clientSize / 2 - areaSize / 2;
   }
}
