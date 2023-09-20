using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp_JoshuaRea.Classes
{
    public class CustomerInfo
    {
        public int customerId;
        public string customerName;
        public string address;
        public string address2;
        public string city;
        public string country;
        public string postalCode;
        public string phone;

        public static DataTable customers = new DataTable();

        public CustomerInfo() { }

        public static DataTable GetCustomerInfo()
        {
            string qry = 
                "SELECT customerId, customerName, address, address2, city, country, postalCode, phone " +
                "FROM customer " +
                "INNER JOIN address " +
                "ON customer.addressId = address.addressId " +
                "INNER JOIN city " +
                "ON address.cityId = city.cityId " +
                "INNER JOIN country " +
                "ON city.countryId = country.countryId";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

            adp.Fill(customers);

            return customers;
        }
    }
}
