using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlisavetAdmin
{
    public partial class Form1 : Form
    {
        private static string path = $"{Environment.CurrentDirectory}//config.txt";
        private static StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
        private static string loginpassword = (sr.ReadLine());
        public Form1()
        {
            InitializeComponent();
        }

        private void просмотретьОценкиВрачейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShowRating form = new FormShowRating();
            form.ShowDialog();
        }

        private void добавитьВрачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd();
            form.Owner = this;
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mass = loginpassword.Split('|');
            if((loginTextBox.Text == mass[0]) && (passwordTextBox.Text == mass[1]))
            {
                menuStrip1.Enabled = true;
                label1.Visible = label2.Visible = loginTextBox.Visible = passwordTextBox.Visible = enterButton.Visible = false;
                label3.Visible = true;
            }
        }
    }
}
