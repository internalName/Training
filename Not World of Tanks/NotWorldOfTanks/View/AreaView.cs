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


       public void ResetSize()
       {
           _area.ResetSize();
       }

       public int LocationArea(int clientSize, int areaSize) => clientSize / 2 - areaSize / 2;
   }
}
