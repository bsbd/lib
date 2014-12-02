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

namespace BD6
{
    public partial class Books_info : Form
    {

        public int j = 0;
        public int x = 1;
        public int y = 5;
        public int del = 0;
        public int count = 0;
        
        public Books_info()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetComments();
        }

        void Standart()
        {
            x = 1;
            j = 0;
            y = 5;
            label11.Text = x.ToString();
            button5.Enabled = false;
            button6.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Standart();
            dataGridView1.DataSource = GetComments();
            create_comb();
        }

        DataTable GetComments()
        {
            DataTable dt = new DataTable();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM books_info ORDER BY id_books LIMIT " + y + " offset " + j, Program.conn);
                NpgsqlCommand command1 = new NpgsqlCommand("SELECT COUNT(*) FROM books_info", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                count = Convert.ToInt32(command1.ExecuteScalar());
                del = count / 5;
                dt.Load(dr);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            return dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            j = j - 5;
            y = 5;
            x = x - 1;
            label11.Text = x.ToString();
            dataGridView1.DataSource = GetComments();
            if (x == 1) button5.Enabled = false;
            if (x != 1) button5.Enabled = true;
            button6.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            j = j + 5;
            if (x == del) y = count % 5;
            x = x + 1;
            label11.Text = x.ToString();
            button5.Enabled = true;
            dataGridView1.DataSource = GetComments();
            if (del < x) button6.Enabled = false;
            if (del > x) button6.Enabled = true;
            y = 5;
        }

        void create_comb()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            try
            {
                Program.conn.Open();
                NpgsqlCommand command2 = new NpgsqlCommand("SELECT * FROM books", Program.conn);
                NpgsqlDataReader dr2 = command2.ExecuteReader();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM filials", Program.conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    comboBox3.Items.Add(column.HeaderText);
                comboBox3.Items.Remove(0);
                while (dr2.Read())
                {
                    string id_books = dr2.GetValue(0).ToString();
                    comboBox2.Items.Add(id_books);
                }
                while (dr.Read())
                {
                    string id_filials = dr.GetValue(0).ToString();
                    comboBox1.Items.Add(id_filials);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboBox1.Items.Add("");
            comboBox2.Items.Add("");
            comboBox3.Items.Add("");
            comboBox3.Items.Remove("id_books");
            Program.conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                string sqlQuery = "INSERT INTO books_info(id_books, author,tittle, genre,pages_number,count,id_filials) VALUES (@add1, @add2, @add3, @add4, @add5, @add6, @add7)";
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                try
                {
                    command.Parameters.Add("@add1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                    command.Parameters.Add("@add2", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox1.Text;
                    command.Parameters.Add("@add3", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox2.Text;
                    command.Parameters.Add("@add4", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox3.Text;
                    command.Parameters.Add("@add5", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(maskedTextBox4.Text);
                    command.Parameters.Add("@add6", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(maskedTextBox5.Text);
                    command.Parameters.Add("@add7", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox1.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Sucsess!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }               
                maskedTextBox1.Text = string.Empty;
                maskedTextBox2.Text = string.Empty;
                maskedTextBox3.Text = string.Empty;
                maskedTextBox4.Text = string.Empty;
                maskedTextBox5.Text = string.Empty;
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            Standart();
            dataGridView1.DataSource = GetComments();
            create_comb();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                string sqlQuery = "DELETE FROM books_info WHERE id_books = @select";
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                try
                {
                    command.Parameters.Add("@select", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Sucsess!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                maskedTextBox1.Text = string.Empty;
                maskedTextBox2.Text = string.Empty;
                maskedTextBox3.Text = string.Empty;
                maskedTextBox4.Text = string.Empty;
                maskedTextBox5.Text = string.Empty;
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            Standart();
            dataGridView1.DataSource = GetComments();
            create_comb();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Program.conn.Open();
                if (comboBox3.Text == "author")
                {                   
                    string sqlQuery = "UPDATE books_info SET author = @select1 WHERE id_books = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox1.Text;
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "tittle")
                {
                    string sqlQuery = "UPDATE books_info SET tittle = @select1 WHERE id_books = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox2.Text;
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "genre")
                {
                    string sqlQuery = "UPDATE books_info SET genre = @select1 WHERE id_books = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Varchar).Value = maskedTextBox3.Text;
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "pages_number")
                {
                    string sqlQuery = "UPDATE books_info SET pages_number = @select1 WHERE id_books = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(maskedTextBox4.Text);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "count")
                {
                    string sqlQuery = "UPDATE count SET pages_number = @select1 WHERE id_books = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(maskedTextBox5.Text);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox3.Text == "id_filials")
                {
                    string sqlQuery = "UPDATE count SET id_filials = @select1 WHERE id_books = @select2";
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, Program.conn);
                    try
                    {
                        command.Parameters.Add("@select1", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox1.Text);
                        command.Parameters.Add("@select2", NpgsqlTypes.NpgsqlDbType.Integer).Value = Convert.ToInt32(comboBox2.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Sucsess!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                maskedTextBox1.Text = string.Empty;
                maskedTextBox2.Text = string.Empty;
                maskedTextBox3.Text = string.Empty;
                maskedTextBox4.Text = string.Empty;
                maskedTextBox5.Text = string.Empty;
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Program.conn.Close();
            Standart();
            dataGridView1.DataSource = GetComments();
            create_comb();
        }        
    }
}

