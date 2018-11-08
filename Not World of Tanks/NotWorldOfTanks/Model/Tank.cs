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
        private TankView _tankView = default(TankView); 

        public Tank()
        {
            _tankView=new TankView();
        }

        public override void Move()
        {

        }

        public override void Turn()
        {

        }
    }
}
