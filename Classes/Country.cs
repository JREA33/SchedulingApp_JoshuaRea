using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp_JoshuaRea.Classes
{
    class Country
    {
    //Setup Attributes

        public int countryId { get; set; }
        public string country { get; set; }

        //Setup Constructors

        public Country() { }

        public Country(int countryId, string country)
        {
            this.countryId = countryId;
            this.country = country;
        }

    //Methods to Create a New Country

        public static int GetNewCountryID()
        {
            int newCountryID = 0;

            string qry = "SELECT MAX(countryId) AS 'newCountryID' FROM country";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                newCountryID = Convert.ToInt32(rdr["newCountryId"]) + 1;
            }

            rdr.Close();

            return newCountryID;
        }

        public static Country CreateNewCountry(string countryName)
        {

            Country newCountry = new Country(GetNewCountryID(), countryName);

            string qry = $"INSERT INTO country " +
                $"VALUES ('{newCountry.countryId}', '{newCountry.country}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}')";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();

            return newCountry;
        }

    //Method to get data from Country table

        public static Country GetCountry(string countryName)
        {
            Country country = new Country();

            string qry = $"SELECT countryId, country FROM country WHERE country = '{countryName}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    country.countryId = Convert.ToInt32(rdr["countryId"]);
                    country.country = rdr["country"].ToString();
                }
            }

            rdr.Close();

            if (country.countryId == 0)
            {
                country = Country.CreateNewCountry(countryName);
            }

            return country;
        }
    }
}
