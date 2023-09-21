using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp_JoshuaRea.Classes
{
    class Customer
    {
    
    //Setup Attributes

        public int customerId { get; set; }
        public string customerName { get; set; }
        public int addressId { get; set; }
        public int active { get; set; }

        //Setup Constructors

        public Customer() { }

        public Customer(int customerId, string customerName, int addressId, int active)
        {
            this.customerId = customerId;
            this.customerName = customerName;
            this.addressId = addressId;
            this.active = active;
        }

    //Setup Methods to create a New Customer

        public static int GetNewCustomerID()
        {
            int newID = 0;

            string queryString = "SELECT MAX(customerId) AS 'newId' FROM customer";
            MySqlCommand cmd = new MySqlCommand(queryString, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                newID = Convert.ToInt32(rdr[0]);
                newID += 1;
            }
            rdr.Close();

            return newID;
        }

        public static void CreateCustomer(string customerName, int addressId, int active)
        {
            Customer newCustomer = new Customer(GetNewCustomerID(), customerName, addressId, active);

            string qry = $"INSERT INTO customer " +
                $"VALUES ('{newCustomer.customerId}', '{newCustomer.customerName}', '{newCustomer.addressId}', '{newCustomer.active}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}')";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

    //Methods to get data from the customer table

        public static Customer GetCustomerByName(string customerName)
        {
            Customer customer = new Customer();

            string qry = $"SELECT customerId, customerName, addressId, active FROM customer WHERE customerName = '{customerName}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    customer.customerId = Convert.ToInt32(rdr["customerId"]);
                    customer.customerName = rdr["customerName"].ToString();
                    customer.addressId = Convert.ToInt32(rdr["addressId"]);
                    customer.active = Convert.ToInt32(rdr["active"]);
                }

            }

            rdr.Close();

            return customer;
        }

        public static Customer GetCustomerById(int customerId)
        {
            Customer customer = new Customer();

            string qry = $"SELECT customerId, customerName, addressId, active FROM customer WHERE customerId = '{customerId}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    customer.customerId = Convert.ToInt32(rdr["customerId"]);
                    customer.customerName = rdr["customerName"].ToString();
                    customer.addressId = Convert.ToInt32(rdr["addressId"]);
                    customer.active = Convert.ToInt32(rdr["active"]);
                }

            }

            rdr.Close();

            return customer;
        }

        public static List<Customer> GetListCustomers()
        {
            List<Customer> customerList = new List<Customer>();

            string query = "SELECT * FROM customer;";
            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Customer customer = new Customer();

                customer.customerId = Convert.ToInt32(rdr["customerId"]);
                customer.customerName = rdr["customerName"].ToString();
                customer.addressId = Convert.ToInt32(rdr["addressId"]);
                customer.active = Convert.ToInt32(rdr["active"]);

                customerList.Add(customer);
            }

            rdr.Close();

            return customerList;
        }

        //Validate that the customer doesn't already exist

        public static bool ValidateCustomer(string customerName)
        {
            bool customerExists = false;

            string qry = $"SELECT customerId, customerName, addressId, active FROM customer WHERE customerName = '{customerName}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.HasRows)
                {
                    customerExists = true;
                    MessageBox.Show("Customer already exists. Please update customer record.");
                }

            }

            rdr.Close();

            return customerExists;
        }

    //Method to Update Customer

        public static void UpdateCustomer(int customerId, string customerName, int addressId, int active)
        {
            string qry = $"UPDATE customer " +
                $"SET customerName = '{customerName}', " +
                $"addressId = '{addressId}', " +
                $"active = '{active}', " +
                $"lastUpdate = '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"lastUpdateBy = '{Login.currentUser.userName}' " +
                $"WHERE customerId = '{customerId}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

    //Method to delete customer

        public static void DeleteCustomer(int customerId)
        {
            string qry = $"DELETE FROM appointment WHERE customerId = '{customerId}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();

            qry = $"DELETE FROM customer WHERE customerId = '{customerId}'";

            cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }
    }
}
