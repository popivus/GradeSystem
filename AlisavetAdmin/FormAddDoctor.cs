using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlisavetAdmin
{
    public partial class FormAddDoctor : Form
    {
        string filename, format;
        byte[] imageData;
        SqlCommand sqlCommand;
        SqlConnection connection;
        bool photo = false;
        public FormAddDoctor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображение (*.png;*.jpg)|*.png;*.jpg";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog.FileName;
                    format = filename.Split('.').Last();
                    Shakal(filename, 50);
                    using (FileStream fs = new FileStream(filename.Substring(0, filename.Length - 4) + "_50.jpg", FileMode.Open))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                    }
                    File.Delete(filename.Substring(0, filename.Length - 4) + "_50.jpg");
                    pictureBox1.Image = ByteToImage(imageData);
                    photo = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool fieldsFilled = true;
            surnameLabel.Text = "";
            nameLabel.Text = "";
            if (string.IsNullOrWhiteSpace(surnameBox.Text))
            {
                surnameLabel.Text = "Заполните фамилию";
                fieldsFilled = false;
            }
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                nameLabel.Text = "Заполните имя";
                fieldsFilled = false;
            }
            if (fieldsFilled)
            {
                if (photo && checkBox1.Checked)
                {
                    sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = @"INSERT INTO [dbo].[Images] VALUES (@Image, @Format)";
                    sqlCommand.Parameters.Add("@Image", SqlDbType.Image, 1000000);
                    sqlCommand.Parameters.Add("@Format", SqlDbType.NVarChar, 30);
                    sqlCommand.Parameters["@Image"].Value = imageData;
                    sqlCommand.Parameters["@Format"].Value = format;
                    connection = new SqlConnection(DBHelper.connectionString);
                    try
                    {
                        connection.Open();
                        sqlCommand.Connection = connection;
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }
                    finally
                    {
                        connection.Close();
                    }
                    string imageId = DBHelper.CmdScalar("SELECT max(ID_Image) FROM [dbo].[Images]");
                    DBHelper.CmdScalar($"INSERT INTO [dbo].[Employee] (Surname, Name, MiddleName, Image_ID, Description) VALUES ('{FirstToUpper(surnameBox.Text)}', '{FirstToUpper(nameBox.Text)}', '{FirstToUpper(middlenameBox.Text)}', {imageId}, '{descriptionBox.Text}')");
                }
                else DBHelper.CmdScalar($"INSERT INTO [dbo].[Employee] (Surname, Name, MiddleName, Description) VALUES ('{FirstToUpper(surnameBox.Text)}', '{FirstToUpper(nameBox.Text)}', '{FirstToUpper(middlenameBox.Text)}', '{descriptionBox.Text}')");
                FormAdd form = Owner as FormAdd;
                form.showGridView.DataSource = DBHelper.FillDataSet("SELECT * FROM [dbo].[Employee_show]").Tables[0];
                form.showGridView.Columns[0].Visible = false;
                Close();
            }
        }

        private string FirstToUpper(string str)
        {
            string newStr = str.Substring(0, 1).ToUpper() + str.Remove(0, 1).ToLower();
            return newStr;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureBox1.Enabled = true;
                photoButton.Enabled = true;
            }
            else
            {
                pictureBox1.Enabled = false;
                photoButton.Enabled = false;
            }
        }

        private void FormAddDoctor_Load(object sender, EventArgs e)
        {

        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
        
        static void Shakal(string address, long stepenShakala)
        {
            using (Bitmap bmp = new Bitmap(address))
                bmp.Save(
                    Path.ChangeExtension(address, "").Trim('.') + $"_{stepenShakala}.jpg",
                    ImageCodecInfo.GetImageEncoders()[1],
                    new EncoderParameters()
                    {
                        Param = new EncoderParameter[]
                        {
                            new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L - stepenShakala)
                        }
                    });
        }
    }
}
