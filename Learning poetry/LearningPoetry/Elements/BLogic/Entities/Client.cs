using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using LearningPoetry.Elements;

namespace LearningPoetry.Entities
{
     abstract class Client
    {
        private string _name, _lName;
        private EnumClientLevelAcces _clientLevelAcces;

        public Client()
        {
            
        }

        public string Name
        {
            get => _name;
            protected set => _name = value;
        }

        public string LName
        {
            get => _lName;
            protected set => _lName = value;
        }


    }
}
