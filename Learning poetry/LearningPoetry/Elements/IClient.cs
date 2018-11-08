using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPoetry.Elements
{
    internal interface IClient
    {
        bool CheckClient(string name, string lName);
        void CreateNewUser(string name,string password);
    }
}
