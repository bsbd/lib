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

namespace BD8
{
    public partial class Sessions : Form
    {

        public int j = 0;
        public int x = 1;
        public int y = 15;
        public int del = 0;
        public int count = 0;
        
        public Sessions()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetComments();
        }

        void Standart()
        {
            x = 1;
            j = 0;
            y = 15;
            label1.Text = x.ToString();
            button1.Enabled = false;
            button2.Enabled = true;
        }

        DataTable GetComments()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM sessions ORDER BY id_sessions LIMIT " + y + " offset " + j, Program.conn);
                NpgsqlCommand command1 = new NpgsqlCommand("SELECT COUNT(*) FROM sessions", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                count = Convert.ToInt32(command1.ExecuteScalar());
                del = count / 15;
                dt.Load(dr);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            j = j - 15;
            y = 15;
            x = x - 1;
            label1.Text = x.ToString();
            dataGridView1.DataSource = GetComments();
            if (x == 1) button1.Enabled = false;
            if (x != 1) button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            j = j + 15;
            if (x == del) y = count % 15;
            x = x + 1;
            label1.Text = x.ToString();
            button1.Enabled = true;
            dataGridView1.DataSource = GetComments();
            if (del < x) button2.Enabled = false;
            if (del > x) button2.Enabled = true;
            y = 15;
        }
    }
}

