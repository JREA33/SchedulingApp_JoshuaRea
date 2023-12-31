﻿using SchedulingApp_JoshuaRea.Classes;
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
    public partial class UpdateAppointment : Form
    {
        //Create an instance of Open Main screen to refresh Customer DataGridView

        Main main = (Main)Application.OpenForms["Main"];

        //Initialize Window
        public UpdateAppointment(Appointment appointment)
        {
            InitializeComponent();

            Customer customer = Customer.GetCustomerById(appointment.customerId);
            User user = User.GetUserById(appointment.userId);

            txtID.Text = appointment.appointmentId.ToString();
            cmbCustomer.Text = customer.customerName;
            cmbUser.Text = user.userName;
            txtTitle.Text = appointment.title;
            txtDescription.Text = appointment.description;
            txtLocation.Text = appointment.location;
            txtContact.Text = appointment.contact;
            txtType.Text = appointment.type;
            txtURL.Text = appointment.url;
            dateStart.Value = appointment.start;
            dateEnd.Value = appointment.end;

            //Populate Combo boxes
            List<Customer> customers = Customer.GetListCustomers();

            //Lambda expressions to populate Customers combo box
            List<string> customerNames = customers.Select(m => m.customerName).ToList();

            customerNames.ForEach(i => cmbCustomer.Items.Add(i));

            List<User> users = User.GetListUsers();

            //Lambda expressions to populate Users combo box
            List<string> userNames = users.Select(m => m.userName).ToList();

            userNames.ForEach(i => cmbUser.Items.Add(i));

        }

        //Validate that fields are not blank and that dates are valid
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
            if (dateStart.Value > dateEnd.Value)
            {
                MessageBox.Show("Appointment start date cannot be greater than end date.");
                return true;
            }

            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateFields()) { }
            else
            {
                //Get customer and user
                Customer customer = Customer.GetCustomerByName(cmbCustomer.Text);
                User user = User.GetUserByName(cmbUser.Text);

                //Create new appointment
                Appointment newAppointment = new Appointment(Convert.ToInt32(txtID.Text), customer.customerId, user.userId, txtTitle.Text, txtDescription.Text, txtLocation.Text, txtContact.Text, txtType.Text, txtURL.Text, dateStart.Value, dateEnd.Value);

                //Check if the dates are within business hours
                if (Appointment.ValidateBusinessHours(newAppointment))
                {
                    //Check if appointment dates overlap with another appointment
                    if (Appointment.ValidateNoOverlap(newAppointment))
                    {
                        Appointment.UpdateAppointment(newAppointment);

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
    }
}
