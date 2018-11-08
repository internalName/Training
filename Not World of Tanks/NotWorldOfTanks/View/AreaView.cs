using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotWorldOfTanks.Model;

namespace NotWorldOfTanks.View
{
    public sealed class AreaView
   {
       private Area _area = default(Area);

       public Size AreaSize => new Size(_area.Height,_area.Width);

        public AreaView(Size size, int numOfTanks, int numOfBonuses, int speed)
        {
            _area=new Area(size.Height,size.Width);
        }

       public Size SetScale(Size formSize,Size gameArea)
       {
           if (_area.Height > formSize.Height - 100 ||
               _area.Width > formSize.Width - 100)
           {
               ResetSize();
           }
           if (_area.Height < 210 || _area.Width < 210) gameArea = new Size(500, 500);

            if (Math.Ceiling((double)gameArea.Height / 30) > gameArea.Height / 30)
           {
               _area=new Area(gameArea.Height/30*30, gameArea.Width/30*30);
               return AreaSize;
           }

           _area = new Area(gameArea.Height, gameArea.Width);

            return AreaSize;
       }

       public void ResetSize()
       {
           _area.ResetSize();
       }

       public int LocationArea(int clientSize, int areaSize) => clientSize / 2 - areaSize / 2;
   }
}
