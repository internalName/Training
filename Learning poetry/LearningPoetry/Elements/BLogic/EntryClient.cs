using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningPoetry.Elements.DAO;

namespace LearningPoetry.Elements.BLogic
{
    internal sealed class EntryClient : IClient
    {
        private ClientDAO _clientDAO=default(ClientDAO);

        public EntryClient()
        {
            _clientDAO=new ClientDAO();
        }

        public EntryClient(string name,string password)
        {
            _clientDAO = new ClientDAO();
            if(CheckClient(name,password)) Entry(name);
        }

        public bool CheckClient(string name, string password)
            => _clientDAO.CheckClient(name, password);

        public void CreateNewUser(string name, string password)
        {
            if (!CheckClient(name, password))
            {
                _clientDAO.CreateNewUser(name,password);
                Entry(name);
            }
            else System.Windows.Forms.MessageBox.Show($"Ups! {name}");

        }

        public void Entry(string name)
        {
            System.Windows.Forms.MessageBox.Show($"Приветствую {name}");
        }
    }
}
