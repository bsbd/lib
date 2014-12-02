using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace BD2
{
    public partial class Autorization : Form
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {                                               /*169.254.203.166*/
                Program.conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=" + textBox1.Text + ";Password=" + textBox2.Text + ";Database=librarys;");
                Program.conn.Open();               
                string sqlQuery = "INSERT INTO sessions(role,time) values (@add, default)";
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                try
                {
                    command.Parameters.Add("@add", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox1.Text;                    
                    command.ExecuteNonQuery();
                    MessageBox.Show("Sucsess!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Program.conn.Close();
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                Tables form = new Tables();
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
