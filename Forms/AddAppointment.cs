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
    public partial class AddAppointment : Form
    {
        //Create an instance of Open Main screen to refresh Customer DataGridView

        Main main = (Main)Application.OpenForms["Main"];

        public AddAppointment()
        {
            InitializeComponent();

            txtID.Text = Appointment.GetNewAppointmentID().ToString();

            foreach (DataGridViewRow row in main.dgvCustomers.Rows)
            {
                cmbCustomer.Items.Add(row.Cells["customerName"].Value.ToString());
            }

            DataTable users = User.GetListUsers();

            foreach (DataRow row in users.Rows)
            {
                cmbUser.Items.Add(row["userName"].ToString());
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(cmbCustomer.Text))
            {
                MessageBox.Show("Please enter a Customer.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(cmbUser.Text))
            {
                MessageBox.Show("Please enter a User.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a Title.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a Description.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Please enter a Location.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtContact.Text))
            {
                MessageBox.Show("Please enter a Contact.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Please enter a Type.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtURL.Text))
            {
                MessageBox.Show("Please enter a URL.");
                return true;
            }

            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateFields()) { }
            else
            {
                Customer customer = Customer.GetCustomerByName(cmbCustomer.Text);
                User user = User.GetUserByName(cmbUser.Text);
                Appointment newAppointment = new Appointment(Convert.ToInt32(txtID.Text), customer.customerId, user.userId, txtTitle.Text, txtDescription.Text, txtLocation.Text, txtContact.Text, txtType.Text, txtURL.Text, dateStart.Value, dateEnd.Value);

                if (Appointment.ValidateBusinessHours(newAppointment))
                {
                    if (Appointment.ValidateNoOverlap(newAppointment))
                    {
                        Appointment.CreateAppointment(newAppointment);

                        main.RefreshAppointmentData();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Appointment times overlap another appointment for this user.");
                    }
                }
                else
                {
                    MessageBox.Show("Appointment is scheduled outside of business hours 8 AM to 5 PM.");
                }  
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
