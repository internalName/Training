using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPoetry.Elements.DAO
{
   internal sealed class ClientDAO : IClient
   {
       private List<string> _nameList;
       private const string connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Poetry;Integrated Security=True";
        

        public ClientDAO()
        {
            _nameList=new List<string>();
        }

        public bool CheckClient(string name, string password)
        {
            using (var cn = new SqlConnection(connString))
            {
                cn.Open();
                var command = new SqlCommand($@"SELECT * FROM dbo.Clients where Name='{name}'", cn);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //var command_search_passsword_contains = new SqlCommand($@"SELECT Password FROM dbo.Clients where Name='{name}'", cn);
                            //if (command_search_passsword_contains.ExecuteReader().HasRows)
                            //{
                            //while (command_search_passsword_contains.ExecuteReader().Read())
                            //{
                            //    if (command_search_passsword_contains.ExecuteReader().GetValue(0).Equals(password))
                            //        return true;
                            _nameList.Add(reader.GetValue(0).ToString());
                                //else
                                //{
                                //    System.Windows.Forms.MessageBox.Show("Neveren parol");
                                //    //return false;
                                //}
                            }
                        }
                    else if(_nameList.Count==0)
                    {
                        System.Windows.Forms.MessageBox.Show($"Такого пользователя не существует");

                        return false;
                    }
                }
                var command_search_passsword_contains = new SqlCommand($@"SELECT Password FROM dbo.Clients where Name='{name}'", cn);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.GetValue(0).Equals(password)) return true;   
                        }
                    }
                }
                    cn.Close();
            }
          
            return false;
        }

        public void CreateNewUser(string name, string password)
        {
            using (var cn = new SqlConnection(connString))
            {
                cn.Open();
                var command = new SqlCommand($@"insert into dbo.Clients (Name, Password) values ('{name}','{password}');", cn);
                cn.Close();
            }
        }
    }
}
