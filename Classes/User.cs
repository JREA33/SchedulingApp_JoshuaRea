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

    //Setup Attributes

        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int active { get; set; }
        public DateTime createDate { get; set; }
        public string createdBy { get; set; }
        public DateTime lastUpdate { get; set; }
        public string lastUpdateBy { get; set; }

    //Setup Constructor

        public User() { }

    //Validate User exists in database by userName and password

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

    //Methods to get data from user table

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
