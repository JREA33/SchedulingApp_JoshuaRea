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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            //Populate appointment data
            RefreshAppointmentData();

            //Set data source for datagridview dgvCustomers
            dgvCustomers.DataSource = CustomerInfo.GetCustomerInfo();
            
            //Format dgvCustomers

            dgvCustomers.Columns["customerId"].HeaderText = "Customer ID";
            dgvCustomers.Columns["customerName"].HeaderText = "Name";
            dgvCustomers.Columns["address"].HeaderText = "Address";
            dgvCustomers.Columns["address2"].HeaderText = "Address2";
            dgvCustomers.Columns["city"].HeaderText = "City";
            dgvCustomers.Columns["country"].HeaderText = "Country";
            dgvCustomers.Columns["postalCode"].HeaderText = "Postal Code";
            dgvCustomers.Columns["phone"].HeaderText = "Phone";

            //Format dgvAppointments

            dgvAppointments.Columns["appointmentId"].HeaderText = "Appointment ID";
            dgvAppointments.Columns["customerId"].HeaderText = "Customer ID";
            dgvAppointments.Columns["userId"].HeaderText = "User ID";
            dgvAppointments.Columns["title"].HeaderText = "Title";
            dgvAppointments.Columns["description"].HeaderText = "Description";
            dgvAppointments.Columns["location"].HeaderText = "Location";
            dgvAppointments.Columns["contact"].HeaderText = "Contact";
            dgvAppointments.Columns["type"].HeaderText = "Type";
            dgvAppointments.Columns["url"].HeaderText = "URL";
            dgvAppointments.Columns["start"].HeaderText = "Start";
            dgvAppointments.Columns["end"].HeaderText = "End";
        }

        //Methods to refresh grids on main page

        public void RefreshCustomerGrid()
        {
            CustomerInfo.customers.Clear();
            dgvCustomers.DataSource = CustomerInfo.GetCustomerInfo();
        }

        public void RefreshAppointmentData()
        {
            if (rbAll.Checked)
            {
                dgvAppointments.DataSource = Appointment.GetAllAppointments();
            }
            if (rbMonth.Checked)
            {
                dgvAppointments.DataSource = Appointment.GetMonthsAppointments();
            }
            if (rbWeek.Checked)
            {
                dgvAppointments.DataSource = Appointment.GetWeeksAppointments();
            }
        }

        //Methods for customer buttons

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            new AddCustomer().ShowDialog();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            int currentCustomerId = Convert.ToInt32(dgvCustomers.CurrentRow.Cells["customerId"].Value);

            Customer.DeleteCustomer(currentCustomerId);

            RefreshCustomerGrid();
            RefreshAppointmentData();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            CustomerInfo currentCustomer = new CustomerInfo();

            currentCustomer.customerId = Convert.ToInt32(dgvCustomers.CurrentRow.Cells["customerId"].Value);
            currentCustomer.customerName = dgvCustomers.CurrentRow.Cells["customerName"].Value.ToString();
            currentCustomer.address = dgvCustomers.CurrentRow.Cells["address"].Value.ToString();
            currentCustomer.address2 = dgvCustomers.CurrentRow.Cells["address2"].Value.ToString();
            currentCustomer.city = dgvCustomers.CurrentRow.Cells["city"].Value.ToString();
            currentCustomer.country = dgvCustomers.CurrentRow.Cells["country"].Value.ToString();
            currentCustomer.postalCode = dgvCustomers.CurrentRow.Cells["postalCode"].Value.ToString();
            currentCustomer.phone = dgvCustomers.CurrentRow.Cells["phone"].Value.ToString();

            new UpdateCustomer(currentCustomer).ShowDialog();
        }

        //Methods to check radio buttons for appointment grid

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            RefreshAppointmentData();
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            RefreshAppointmentData();
        }

        private void rbWeek_CheckedChanged(object sender, EventArgs e)
        {
            RefreshAppointmentData();
        }

        //Methods for appointment buttons

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            new AddAppointment().ShowDialog();
        }

        private void btnDeleteAppointment_Click(object sender, EventArgs e)
        {
            Appointment.DeleteAppointment(Convert.ToInt32(dgvAppointments.CurrentRow.Cells["appointmentId"].Value));
            RefreshAppointmentData();
        }

        private void btnUpdateAppointment_Click(object sender, EventArgs e)
        {
            var currentRow = dgvAppointments.CurrentRow;

            Appointment updateAppointment = new Appointment();

            updateAppointment.appointmentId = Convert.ToInt32(currentRow.Cells["appointmentId"].Value);
            updateAppointment.customerId = Convert.ToInt32(currentRow.Cells["customerId"].Value);
            updateAppointment.userId = Convert.ToInt32(currentRow.Cells["userId"].Value);
            updateAppointment.title = currentRow.Cells["title"].Value.ToString();
            updateAppointment.description = currentRow.Cells["description"].Value.ToString();
            updateAppointment.location = currentRow.Cells["location"].Value.ToString();
            updateAppointment.contact = currentRow.Cells["contact"].Value.ToString();
            updateAppointment.type = currentRow.Cells["type"].Value.ToString();
            updateAppointment.url = currentRow.Cells["url"].Value.ToString();
            updateAppointment.start = Convert.ToDateTime(currentRow.Cells["start"].Value);
            updateAppointment.end = Convert.ToDateTime(currentRow.Cells["end"].Value);

            new UpdateAppointment(updateAppointment).ShowDialog();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            new Reports().ShowDialog();
        }
    }
}