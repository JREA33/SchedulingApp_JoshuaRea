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

    //Setup Attributes

        public int customerId { get; set; }
        public string customerName { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }

        //Setup DataTable to hold customer info

        public static DataTable customers = new DataTable();

    //Create Constructor

        public CustomerInfo() { }

    //Fill DataTable with customer info

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
