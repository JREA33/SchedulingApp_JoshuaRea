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
    public partial class Reminder : Form
    {
        public Reminder(DataTable reminders)
        {
            InitializeComponent();

            dgvReminders.DataSource = reminders;

            dgvReminders.Columns["title"].HeaderText = "Title";
            dgvReminders.Columns["description"].HeaderText = "Description";
            dgvReminders.Columns["start"].HeaderText = "Start";
            dgvReminders.Columns["end"].HeaderText = "End";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
