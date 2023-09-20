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
    public partial class UpdateCustomer : Form
    {

        //Create an instance of Open Main screen to refresh Customer DataGridView

        Main main = (Main)Application.OpenForms["Main"];

        public UpdateCustomer(CustomerInfo customer)
        {
            InitializeComponent();

            txtID.Text = customer.customerId.ToString();
            txtName.Text = customer.customerName;
            txtAddress.Text = customer.address;
            txtAddress2.Text = customer.address2;
            txtCity.Text = customer.city;
            txtCountry.Text = customer.country;
            txtPostalCode.Text = customer.postalCode;
            txtPhone.Text = customer.phone;
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a Customer Name.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please enter an Address.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                MessageBox.Show("Please enter a City.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtCountry.Text))
            {
                MessageBox.Show("Please enter a Country.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtPostalCode.Text))
            {
                MessageBox.Show("Please enter a Postal Code.");
                return true;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please enter a Phone Number.");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateFields()) { }

            else
            {
                Country country = Country.GetCountry(txtCountry.Text);

                City city = City.GetCity(txtCity.Text, country.countryId);

                Address address = Address.GetAddress(txtAddress.Text, txtAddress2.Text, city.cityId, txtPostalCode.Text, txtPhone.Text);

                Customer.UpdateCustomer(Convert.ToInt32(txtID.Text), txtName.Text, address.addressId, 1);

                main.RefreshCustomerGrid();

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
