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
    public partial class AddCustomer : Form
    {
        //Create an instance of Open Main screen to refresh Customer DataGridView

        Main main = (Main)Application.OpenForms["Main"];

        public AddCustomer()
        {
            InitializeComponent();

            txtID.Text = Customer.GetNewCustomerID().ToString();
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
                bool customerExists = Customer.ValidateCustomer(txtName.Text);

                if (customerExists == false)
                {
                    Country country = Country.GetCountry(txtCountry.Text);

                    City city = City.GetCity(txtCity.Text, country.countryId);

                    Address address = Address.GetAddress(txtAddress.Text, txtAddress2.Text, city.cityId, txtPostalCode.Text, txtPhone.Text);

                    Customer.CreateCustomer(txtName.Text, address.addressId, 1);

                    main.RefreshCustomerGrid();

                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
