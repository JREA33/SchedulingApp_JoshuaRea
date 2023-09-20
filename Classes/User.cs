using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp_JoshuaRea.Classes
{
    public class User
    {
        public int userId;
        public string userName;
        public string password;
        public int active;
        public DateTime createDate;
        public string createdBy;
        public DateTime lastUpdate;
        public string lastUpdateBy;

        public User() { }

        public static User ValidateUser(string userName, string password)
        {
            User currentUser = new User();

            string qry = $"SELECT * FROM user WHERE userName = '{userName}' and password = '{password}'";
            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    currentUser.userId = Convert.ToInt32(rdr["userId"]);
                    currentUser.userName = rdr["userName"].ToString();
                    currentUser.password = rdr["password"].ToString();
                    currentUser.active = Convert.ToInt32(rdr["active"]);
                    currentUser.createDate = Convert.ToDateTime(rdr["createDate"]);
                    currentUser.createdBy = rdr["createdBy"].ToString();
                    currentUser.lastUpdate = Convert.ToDateTime(rdr["lastUpdate"]);
                    currentUser.lastUpdateBy = rdr["lastUpdateBy"].ToString();
                }
            }

            rdr.Close();

            return currentUser;
        }

        public static DataTable GetListUsers()
        {
            DataTable users = new DataTable();

            string query = "SELECT * FROM user;";
            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(users);

            return users;
        }

        public static User GetUserByName(string userName)
        {
            User user = new User();

            string qry = $"SELECT userId, userName FROM user WHERE userName = '{userName}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                user.userId = Convert.ToInt32(rdr["userId"]);
                user.userName = rdr["userName"].ToString();
            }

            rdr.Close();

            return user;
        }

        public static User GetUserById(int userId)
        {
            User user = new User();

            string qry = $"SELECT userId, userName FROM user WHERE userId = '{userId}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                user.userId = Convert.ToInt32(rdr["userId"]);
                user.userName = rdr["userName"].ToString();
            }

            rdr.Close();

            return user;
        }
    }
}
