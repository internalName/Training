using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningPoetry.Elements.DAO;

namespace LearningPoetry.Entities
{
   internal class Admin:Client
    {

        public Admin()
        {
            var client=new ClientDAO();
        }
    }
}
