using BD3;
using BD4;
using BD5;
using BD6;
using BD7;
using BD8;
using BD9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD2
{
    public partial class Tables : Form
    {
        public Tables()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Readership form = new Readership();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Readership_info form = new Readership_info();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Books form = new Books();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Books_info form = new Books_info();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Filials form = new Filials();
            form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Librarians form = new Librarians();
            form.ShowDialog();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Get_books form = new Get_books();
            form.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Sessions form = new Sessions();
            form.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Stats form = new Stats();
            form.ShowDialog();
        }
    }
}
