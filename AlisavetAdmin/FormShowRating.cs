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
    public partial class FormShowRating : Form
    {
        public FormShowRating()
        {
            InitializeComponent();
        }

        private void FormShowRating_Load(object sender, EventArgs e)
        {
            doctorBox.DataSource = DBHelper.FillDataSet("SELECT * FROM [dbo].[Employee_show_for_rating]").Tables[0];
            doctorBox.ValueMember = "ID";
            doctorBox.DisplayMember = "ФИО";
            filialBox.DataSource = DBHelper.FillDataSet("SELECT * FROM [dbo].[Branch]").Tables[0];
            filialBox.ValueMember = "ID_Branch";
            filialBox.DisplayMember = "Name";
        }

        private void acceptBtn_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            if (!allFilialsBox.Checked) dataSet = DBHelper.FillDataSet($"SELECT * FROM [dbo].[Rates_show] WHERE [Branch_ID] = {filialBox.SelectedValue} AND [Employee_ID] = {doctorBox.SelectedValue} AND [Дата] >= '{fromDatePicker.Value.ToString("dd.MM.yyyy")}' AND [Дата] <= '{toDatePicker.Value.ToString("dd.MM.yyyy")}'");
            else dataSet = DBHelper.FillDataSet($"SELECT * FROM [dbo].[Rates_show] WHERE [Employee_ID] = {doctorBox.SelectedValue} AND [Дата] >= '{fromDatePicker.Value.ToString("dd.MM.yyyy")}' AND [Дата] <= '{toDatePicker.Value.ToString("dd.MM.yyyy")}'");
            ratesGridView.DataSource = dataSet.Tables[0];
            ratesGridView.Columns[2].Visible = false;
            ratesGridView.Columns[3].Visible = false;
            int count = 0;
            double allRates = 0;
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                count++;
                allRates += Convert.ToDouble(row.ItemArray[1].ToString());
                if (Convert.ToInt32(row.ItemArray[1]) > 6) ratesGridView.Rows[count - 1].DefaultCellStyle.BackColor = Color.Green;
                if (Convert.ToInt32(row.ItemArray[1]) < 7 && Convert.ToInt32(row.ItemArray[1]) > 4) ratesGridView.Rows[count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                if (Convert.ToInt32(row.ItemArray[1]) < 5)
                {
                    ratesGridView.Rows[count - 1].DefaultCellStyle.BackColor = Color.Red;
                    ratesGridView.Rows[count - 1].DefaultCellStyle.ForeColor = Color.White;
                }
            }
            double average = Math.Round(allRates / count, 1);
            averageRatingLabel.Text = average.ToString();
            if (!char.IsDigit(averageRatingLabel.Text[0])) averageRatingLabel.Text = "нет оценок";
        }

        private void allFilialsBox_CheckedChanged(object sender, EventArgs e)
        {
            if (allFilialsBox.Checked) filialBox.Enabled = false;
            else filialBox.Enabled = true;
        }
    }
}
