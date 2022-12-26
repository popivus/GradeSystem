using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlisavetAdmin
{
    public partial class FormAdd : Form
    {
        public FormAdd()
        {
            InitializeComponent();
        }
        private void editBtn_Click(object sender, EventArgs e)
        {
            if (showGridView.SelectedRows.Count != 0)
            {
                FormEditDoctor form = new FormEditDoctor();
                form.Owner = this;
                form.ShowDialog();
            }
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            showGridView.DataSource = DBHelper.FillDataSet("SELECT * FROM [dbo].[Employee_show]").Tables[0];
            showGridView.Columns[0].Visible = false;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (showGridView.SelectedRows.Count != 0)
            {
                DBHelper.CmdScalar($"DELETE FROM [Rates] WHERE [Employee_ID] = {showGridView.SelectedRows[0].Cells[0].Value}");
                DBHelper.CmdScalar($"DELETE FROM [Employee] WHERE [ID_Employee] = {showGridView.SelectedRows[0].Cells[0].Value}");
                showGridView.DataSource = DBHelper.FillDataSet("SELECT * FROM [dbo].[Employee_show]").Tables[0];
                showGridView.Columns[0].Visible = false;
            }
        }

        private void addBtn_Click_1(object sender, EventArgs e)
        {
            FormAddDoctor form = new FormAddDoctor();
            form.Owner = this;
            form.ShowDialog();
        }
    }
}
