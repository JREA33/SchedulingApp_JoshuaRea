using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp_JoshuaRea.Classes
{
    public class Appointment
    {
    //Setup Attributes

        public int appointmentId { get; set; }
        public int customerId { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string contact { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        //Create DataTable to Hold Appointment Data for dgvAppointments

        public static DataTable appointmentsData = new DataTable();

    //Create Constructors

        public Appointment() { }

        public Appointment(int appointmentId, int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end)
        {
            this.appointmentId = appointmentId;
            this.customerId = customerId;
            this.userId = userId;
            this.title = title;
            this.description = description;
            this.location = location;
            this.contact = contact;
            this.type = type;
            this.url = url;
            this.start = start;
            this.end = end;
        }

    //Methods to fill Appointment DataTable

        public static DataTable GetAllAppointments()
        {
            appointmentsData.Clear();

            string qry = "SELECT appointmentId, customerId, userId, title, description, location, contact, type, url, start, end " +
                "FROM appointment";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

            adp.Fill(appointmentsData);

            for (int i = 0; i < appointmentsData.Rows.Count; i++)
            {
                DateTime start = (DateTime)appointmentsData.Rows[i]["start"];
                appointmentsData.Rows[i]["start"] = start.ToLocalTime();

                DateTime end = (DateTime)appointmentsData.Rows[i]["end"];
                appointmentsData.Rows[i]["end"] = end.ToLocalTime();

            }

            return appointmentsData;
        }

        public static DataTable GetMonthsAppointments()
        {
            appointmentsData.Clear();

            string qry = "SELECT appointmentId, customerId, userId, title, description, location, contact, type, url, start, end " +
                "FROM appointment " +
                $"WHERE start >= '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}' " +
                $"and start <= '{DateTime.Now.ToUniversalTime().AddMonths(1).ToString("yyyy-MM-dd HH:mm:ss")}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

            adp.Fill(appointmentsData);

            for (int i = 0; i < appointmentsData.Rows.Count; i++)
            {
                DateTime start = (DateTime)appointmentsData.Rows[i]["start"];
                appointmentsData.Rows[i]["start"] = start.ToLocalTime();

                DateTime end = (DateTime)appointmentsData.Rows[i]["end"];
                appointmentsData.Rows[i]["end"] = end.ToLocalTime();

            }

            return appointmentsData;
        }

        public static DataTable GetWeeksAppointments()
        {
            appointmentsData.Clear();

            string qry = "SELECT appointmentId, customerId, userId, title, description, location, contact, type, url, start, end " +
                "FROM appointment " +
                $"WHERE start >= '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}' " +
                $"and start <= '{DateTime.Now.ToUniversalTime().AddDays(7).ToString("yyyy-MM-dd HH:mm:ss")}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

            adp.Fill(appointmentsData);

            for (int i = 0; i < appointmentsData.Rows.Count; i++)
            {
                DateTime start = (DateTime)appointmentsData.Rows[i]["start"];
                appointmentsData.Rows[i]["start"] = start.ToLocalTime();

                DateTime end = (DateTime)appointmentsData.Rows[i]["end"];
                appointmentsData.Rows[i]["end"] = end.ToLocalTime();

            }

            return appointmentsData;
        }

        public static DataTable GetUserAppointments(User user)
        {
            DataTable userAppointmentsData = new DataTable();

            string qry = "SELECT appointmentId as 'Appointment ID', customerId as 'Customer ID', userId as 'User ID', title as 'Title', description as 'Description', location as 'Location', contact as 'Contact', type as 'Type', url as 'URL', start as 'Start', end as 'End' " +
                "FROM appointment " +
                $"WHERE userId = '{user.userId}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

            adp.Fill(userAppointmentsData);

            for (int i = 0; i < userAppointmentsData.Rows.Count; i++)
            {
                DateTime start = (DateTime)userAppointmentsData.Rows[i]["start"];
                userAppointmentsData.Rows[i]["start"] = start.ToLocalTime();

                DateTime end = (DateTime)userAppointmentsData.Rows[i]["end"];
                userAppointmentsData.Rows[i]["end"] = end.ToLocalTime();

            }

            return userAppointmentsData;
        }

    //Methods to Create New Appointments

        public static int GetNewAppointmentID()
        {
            int newId = 0;

            string query = "SELECT MAX(appointmentId) AS 'newId' FROM appointment";

            MySqlCommand cmd = new MySqlCommand(query, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                newId = Convert.ToInt32(rdr["newId"]) + 1;
            }
            rdr.Close();

            return newId;
        }

        public static void CreateAppointment(Appointment appointment)
        {
            string qry = $"INSERT INTO appointment " +
                $"VALUES ('{appointment.appointmentId}', '{appointment.customerId}', '{appointment.userId}', '{appointment.title}', '{appointment.description}', '{appointment.location}', '{appointment.contact}', '{appointment.type}', '{appointment.url}', '{appointment.start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{appointment.end.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}', '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', '{Login.currentUser.userName}')";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

    //Method to Delete Appointment

        public static void DeleteAppointment(int appointmentId)
        {
            string qry = $"DELETE FROM appointment WHERE appointmentId = '{appointmentId}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

    //Method to Update an Appointment

        public static void UpdateAppointment(Appointment appointment)
        {
            string qry =
                $"UPDATE appointment " +
                $"SET " +
                $"customerId = '{appointment.customerId}', " +
                $"userId = '{appointment.userId}', " +
                $"title = '{appointment.title}', " +
                $"description = '{appointment.description}', " +
                $"location = '{appointment.location}', " +
                $"contact = '{appointment.contact}', " +
                $"type = '{appointment.type}', " +
                $"url = '{appointment.url}', " +
                $"start = '{appointment.start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"end = '{appointment.end.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"lastUpdate = '{DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}', " +
                $"lastUpdateBy = '{Login.currentUser.userName}' " +
                $"WHERE appointmentId = '{appointment.appointmentId}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

    //Validation for New Appointments

        public static bool ValidateBusinessHours(Appointment appointment)
        {
            DateTime businessStart = DateTime.Today.AddHours(8);
            DateTime businessEnd = DateTime.Today.AddHours(17);

            DateTime appointmentStart = DateTime.Parse(appointment.start.ToString());
            DateTime appointmentEnd = DateTime.Parse(appointment.end.ToString());

            if (appointmentStart.TimeOfDay >= businessStart.TimeOfDay && appointmentStart.TimeOfDay <= businessEnd.TimeOfDay && appointmentEnd.TimeOfDay > businessStart.TimeOfDay && appointmentEnd.TimeOfDay <= businessEnd.TimeOfDay)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidateNoOverlap(Appointment appointment)
        {
            string qry = $"SELECT * FROM appointment WHERE userId = '{appointment.userId}' and ((start >= '{appointment.start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}' and start <= '{appointment.end.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}') or (end >= '{appointment.start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}' and end <= '{appointment.end.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}'))";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            rdr.Read();

            if (rdr.HasRows)
            {
                rdr.Close();
                return false;
            }
            else
            {
                rdr.Close();
                return true;
            }
        }
    }
}