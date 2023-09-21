using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp_JoshuaRea.Classes
{
    class Address
    {
    //Setup Attributes

        public int addressId { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public int cityId { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }

        //Create Constructors

        public Address() { }

        public Address(int addressId, string address, string address2, int cityId, string postalCode, string phone)
        {
            this.addressId = addressId;
            this.address = address;
            this.address2 = address2;
            this.cityId = cityId;
            this.postalCode = postalCode;
            this.phone = phone;
        }

    //Methods to Get Data from Address Table

        public static int GetNewAddressId()
        {
            int newAddressID = 0;

            string qry = "SELECT MAX(addressId) AS 'newAddressID' FROM address";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                newAddressID = Convert.ToInt32(rdr["newAddressID"]) + 1;
            }

            rdr.Close();

            return newAddressID;
        }

        public static Address GetAddress(string address, string address2, int cityId, string postalCode, string phone)
        {
            Address getAddress = new Address();

            string qry = $"SELECT addressId, address, address2, cityId, postalCode, phone " +
                $"FROM address " +
                $"WHERE address = '{address}' " +
                $"and address2 = '{address2}' " +
                $"and cityId = '{cityId}' " +
                $"and postalCode = '{postalCode}' " +
                $"and phone = '{phone}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    getAddress.addressId = Convert.ToInt32(rdr["addressId"]);
                    getAddress.address = rdr["address"].ToString();
                    getAddress.address2 = rdr["address2"].ToString();
                    getAddress.cityId = Convert.ToInt32(rdr["cityId"]);
                    getAddress.postalCode = rdr["postalCode"].ToString();
                    getAddress.phone = rdr["phone"].ToString();
                }
            }

            rdr.Close();

            if (getAddress.addressId == 0)
            {
                getAddress = CreateNewAddress(address, address2, cityId, postalCode, phone);
            }

            return getAddress;
        }

    //Method to Create a New Address

        public static Address CreateNewAddress(string address, string address2, int cityId, string postalCode, string phone)
        {
            Address newAddress = new Address(GetNewAddressId(), address, address2, cityId, postalCode, phone);

            string qry = "INSERT INTO address " +
                $"VALUES ('{newAddress.addressId}', '{newAddress.address}', '{newAddress.address2}', '{newAddress.cityId}', '{newAddress.postalCode}', '{newAddress.phone}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}')";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();

            return newAddress;
        }
    }
}
