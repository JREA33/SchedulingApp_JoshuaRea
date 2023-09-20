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
        string loginError = "Username and Password Do Not Match";

        public static User currentUser = new User();

        public Login()
        {
            InitializeComponent();

            //CultureInfo.CurrentCulture = new CultureInfo("es");

            SetLanguage();
        }

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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            currentUser = User.ValidateUser(txtUsername.Text, txtPassword.Text);

            if (currentUser.userId != 0)
            {
                string path = "log.txt";
                string log = $"\nUser '{currentUser.userName}' has logged in at '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}'";

                File.AppendAllText(path, log);

                this.Hide();
                MeetingAlert(currentUser);
                new Main().ShowDialog();
                this.Close();
            }
            else
            {
                string path = "log.txt";
                string log = $"\nFailed Login Attempt with User '{txtUsername.Text}' at {DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}'";

                File.AppendAllText(path, log);

                MessageBox.Show(loginError);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
