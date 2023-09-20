using SchedulingApp_JoshuaRea.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp_JoshuaRea
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Start and End Database Connection

            DBConnection.startConnection();
            Application.Run(new Login());
            DBConnection.closeConnection();

        }
    }
}
