using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp_JoshuaRea.Classes
{
    class City
    {
        public int cityId;
        public string city;
        public int countryId;

        public City() { }

        public City(int cityId, string city, int countryId)
        {
            this.cityId = cityId;
            this.city = city;
            this.countryId = countryId;
        }

        public static int GetNewCityID()
        {
            int newCityId = 0;

            string qry = "SELECT MAX(cityId) AS 'newCityId' FROM city";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                newCityId = Convert.ToInt32(rdr["newCityId"]) + 1;
            }

            rdr.Close();

            return newCityId;
        }

        public static City CreateNewCity(string city, int countryId)
        {
            City newCity = new City(GetNewCityID(), city, countryId);

            string qry = $"INSERT INTO city " +
                $"VALUES ('{newCity.cityId}', '{newCity.city}', '{newCity.countryId}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}')";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();

            return newCity;
        }

        public static City GetCity(string city, int countryId)
        {
            City getCity = new City();

            string qry = $"SELECT cityId, city, countryId FROM city WHERE city = '{city}' and countryId = '{countryId}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    getCity.cityId = Convert.ToInt32(rdr["cityId"]);
                    getCity.city = rdr["city"].ToString();
                    getCity.countryId = Convert.ToInt32(rdr["countryId"]);
                }
            }

            rdr.Close();

            if (getCity.cityId == 0)
            {
                getCity = City.CreateNewCity(city, countryId);
            }

            return getCity;
        }
    }
}
