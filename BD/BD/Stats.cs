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

namespace BD9
{
    public partial class Stats : Form
    {
        public Stats()
        {
            InitializeComponent();
        }

        DataTable GetComments()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(id_get) FROM get_books", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        DataTable GetComments1()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT readership_info.name, readership_info.surname, books_info.tittle FROM readership_info INNER JOIN get_books ON get_books.readership_ticket_number = readership_info.ticket_number INNER JOIN books_info ON books_info.id_books = get_books.id_books WHERE books_info.author = 'Mark Twain'", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        DataTable GetComments2()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT publisher, SUM(count) AS Sum_count FROM books LEFT JOIN books_info ON books_info.id_books = books.id_books GROUP BY publisher ORDER BY Sum_count DESC LIMIT 1", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        DataTable GetComments3()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT address, phone, COUNT(filials.id_filials) AS Count_filials FROM filials LEFT JOIN get_books ON get_books.id_filials = filials.id_filials GROUP BY filials.id_filials ORDER BY Count_filials DESC LIMIT 1", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        DataTable GetComments4()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM books_info WHERE genre = 'novel'", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
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
            dataGridView1.DataSource = GetComments();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetComments1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetComments2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetComments3();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetComments4();
        }
    }
}

