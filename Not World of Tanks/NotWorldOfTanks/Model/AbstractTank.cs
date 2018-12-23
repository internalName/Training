using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotWorldOfTanks.Model
{
    internal abstract class AbstractTank:GameObject
    {
        private int Health { get; set; }

        public virtual void Move() { }

        public virtual void Turn() { }

        public virtual void Shoot() { }

        public virtual void MeetingWithTheWall() { }

    }
}
