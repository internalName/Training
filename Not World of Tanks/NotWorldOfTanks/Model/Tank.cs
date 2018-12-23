using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotWorldOfTanks.View;

namespace NotWorldOfTanks.Model
{
    internal sealed class Tank: AbstractTank
    {
        public System.Drawing.Point _PointPosition = default(System.Drawing.Point);

        public System.Drawing.Point PointPosition
        {
            get => _PointPosition;
            set => _PointPosition = value;
        }

        public int X
        {
            get => _PointPosition.X;
            set => _PointPosition.X = value;
        }

        public int Y
        {
            get => _PointPosition.Y;
            set => _PointPosition.Y = value;
        }

        public direction Direction { get; set; }

        public Tank(int x,int y)
        {
            _PointPosition.X = x;
            _PointPosition.Y = y;

            Direction = direction.Up;
        }


        public override void Move()
        {
            if (this.Direction == direction.Up) this.Y -= 1;
            //if(this.Direction==direction.Down)
        }

        public override void Turn()
        {

        }
    }
}
