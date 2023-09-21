using MySql.Data.MySqlClient;
using SchedulingApp_JoshuaRea.Classes;
using SchedulingApp_JoshuaRea.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp_JoshuaRea
{
    public partial class Login : Form
    {
        //Initialize and set login message in english

        string loginError = "Username and Password Do Not Match";

        //Initialize current user

        public static User currentUser = new User();

        public Login()
        {
            InitializeComponent();

            //CultureInfo.CurrentCulture = new CultureInfo("es");

            //Check if language is "es" and convert form to spanish if it is

            SetLanguage();
        }

        //Method to check and set language

        private void SetLanguage()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "es")
            {
                this.Text = "Programador";
                lblLogin.Text = "Iniciar sesión";
                lblUsername.Text = "Nombre de usuario";
                lblPassword.Text = "Contraseña";
                btnLogin.Text = "Iniciar sesión";
                btnCancel.Text = "Cancelar";
                loginError = "El nombre de usuario y la contraseña no coinciden";
            }
        }

        //Method for click of Login button

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Validate user and set as current user
            currentUser = User.ValidateUser(txtUsername.Text, txtPassword.Text);

            //Check if user was valid
            if (currentUser.userId != 0)
            {
                //Create or append to log file
                string path = "log.txt";
                string log = $"\nUser '{currentUser.userName}' has logged in at '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}'";

                File.AppendAllText(path, log);

                //Navigate to main page and display reminders of upcoming meetings
                this.Hide();
                MeetingAlert(currentUser);
                new Main().ShowDialog();
                this.Close();
            }
            else
            {
                //Create or append to log file
                string path = "log.txt";
                string log = $"\nFailed Login Attempt with User '{txtUsername.Text}' at {DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}'";

                File.AppendAllText(path, log);

                MessageBox.Show(loginError);
            }
        }

        //Method for click of cancel button

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Method to display reminders or upcoming meetings
        private void MeetingAlert(User user)
        {
            string currentTimestamp = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
            string alertWindowTimestamp = DateTime.Now.ToUniversalTime().AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss");

            string qry = $"SELECT title, description, start, end FROM appointment WHERE userId = '{user.userId}' and start >= '{currentTimestamp}' and start <= '{alertWindowTimestamp}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            
            if (rdr.HasRows)
            {
                rdr.Close();
                DataTable reminders = new DataTable();
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(reminders);
                new Reminder(reminders).ShowDialog();
            }

            rdr.Close();
        }
    }
}
