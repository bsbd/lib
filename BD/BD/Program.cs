using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

class Program
{
    public static NpgsqlConnection conn;
}

namespace BD2
{

    static class Program1
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        ///  

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Autorization());
        }
    }
}
