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
        public System.Drawing.Point _targetPosition = default(System.Drawing.Point);

        public System.Drawing.Point PointPosition
        {
            get => _targetPosition;
            set => _targetPosition = value;
        }

        public int X
        {
            get => _targetPosition.X;
            set => _targetPosition.X = value;
        }

        public int Y
        {
            get => _targetPosition.Y;
            set => _targetPosition.Y = value;
        }

        public direction Direction { get; set; }

        public Tank(int x,int y)
        {
            _targetPosition.X = x;
            _targetPosition.Y = y;

            Direction = direction.Up;
        }


        public override void Move()
        {

        }

        public override void Turn()
        {

        }
    }
}
