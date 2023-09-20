using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp_JoshuaRea.Classes
{
    public class Appointment
    {
        public int appointmentId;
        public int customerId;
        public int userId;
        public string title;
        public string description;
        public string location;
        public string contact;
        public string type;
        public string url;
        public DateTime start;
        public DateTime end;

        public static DataTable appointmentsData = new DataTable();

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

        public static void DeleteAppointment(int appointmentId)
        {
            string qry = $"DELETE FROM appointment WHERE appointmentId = '{appointmentId}'";

            MySqlCommand cmd = new MySqlCommand(qry, DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

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
    }
}
