using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.Security;

namespace AkıllıAraba.Models.Security
{
    public class PersonelProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string tc)
        {
            var sql = "SELECT * FROM users WHERE tc = " + tc;
            DataTable dt = new DataTable();
            MySqlConnection baglantı = new MySqlConnection("Server=Localhost;Database=smart_car;Uid=root;Pwd=;charset=utf8;");
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand commond = new MySqlCommand();
            commond.CommandText = sql;
            commond.Connection = baglantı;
            adapter.SelectCommand = commond;
            adapter.Fill(dt);
            LoginViewModels model = new LoginViewModels
            {
                id = int.Parse(dt.Rows[0].ItemArray[0].ToString()),
                tc = dt.Rows[0].ItemArray[1].ToString(),
                code = dt.Rows[0].ItemArray[2].ToString(),
                role = int.Parse(dt.Rows[0].ItemArray[6].ToString()),
            };
            adapter.Dispose();
            baglantı.Close();
            return new string[] {model.role.ToString() };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}