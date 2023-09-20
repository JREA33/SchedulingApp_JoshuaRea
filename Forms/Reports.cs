using MySql.Data.MySqlClient;
using SchedulingApp_JoshuaRea.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp_JoshuaRea.Forms
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();

            DataTable users = User.GetListUsers();

            foreach (DataRow row in users.Rows)
            {
                cmbUsers.Items.Add(row["userName"].ToString());
            }

            PopulateCountryCustomers();
            PopulateAppointmentTypes();
        }

        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            User user = User.GetUserByName(cmbUsers.Text);
            dgvUserAppointments.DataSource = Appointment.GetUserAppointments(user);
        }

        private void PopulateCountryCustomers()
        {
            DataTable customerCount = new DataTable();

            string qry = "SELECT country as Country, COUNT(*) as 'Total Customers' FROM customer INNER JOIN address ON address.addressId = customer.addressId INNER JOIN city ON city.cityId = address.cityId INNER JOIN country ON country.countryId = city.countryId GROUP BY country HAVING COUNT('Total Customers') > 0";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(customerCount);

            dgvCountryCustomers.DataSource = customerCount;
        }

        private void PopulateAppointmentTypes()
        {
            DataTable appointmentTypes = new DataTable();

            string qry = "SELECT MONTHNAME(start) as Month, type as Type, COUNT(*) as 'Total Appointments' FROM appointment GROUP BY type, Month;";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            adp.Fill(appointmentTypes);

            dgvAppointmentTypes.DataSource = appointmentTypes;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
