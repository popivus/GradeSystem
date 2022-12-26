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
    public partial class FormEditDoctor : Form
    {
        string filename, format;
        byte[] imageData;
        SqlCommand sqlCommand;
        SqlConnection connection;
        bool photoExist = false, photoChanged = false;
        DataSet dataSet;
        public FormEditDoctor()
        {
            InitializeComponent();
        }

        private void FormEditDoctor_Load(object sender, EventArgs e)
        {
            FormAdd form = Owner as FormAdd;
            dataSet = new DataSet();
            dataSet = DBHelper.FillDataSet($"SELECT * FROM [dbo].[Employee_edit] WHERE [ID] = {form.showGridView.SelectedRows[0].Cells[0].Value}");
            surnameBox.Text = dataSet.Tables[0].Rows[0].ItemArray[1].ToString();
            nameBox.Text = dataSet.Tables[0].Rows[0].ItemArray[2].ToString();
            middlenameBox.Text = dataSet.Tables[0].Rows[0].ItemArray[3].ToString();
            descriptionBox.Text = dataSet.Tables[0].Rows[0].ItemArray[4].ToString();
            if (dataSet.Tables[0].Rows[0].ItemArray[5] as byte[] != null)
            {
                pictureBox1.Image = ByteToImage(dataSet.Tables[0].Rows[0].ItemArray[5] as byte[]);
                photoExist = true;
                checkBox1.Checked = true;
            }
        }

        private void photoButton_Click(object sender, EventArgs e)
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
                }
                pictureBox1.Image = ByteToImage(imageData);
                photoChanged = true;
            }
        }

        private string FirstToUpper(string str)
        {
            string newStr = str.Substring(0, 1).ToUpper() + str.Remove(0, 1).ToLower();
            return newStr;
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
                if (!photoExist && checkBox1.Checked && photoChanged)
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
                    DBHelper.CmdScalar($"UPDATE [dbo].[Employee] SET [Surname] = '{FirstToUpper(surnameBox.Text)}', [Name] = '{FirstToUpper(nameBox.Text)}', [MiddleName] = '{FirstToUpper(middlenameBox.Text)}', [Image_ID] = {imageId}, [Description] = '{descriptionBox.Text}' WHERE [ID_Employee] = {dataSet.Tables[0].Rows[0].ItemArray[0]}");
                }
                else
                {
                    if (!checkBox1.Checked && !photoExist)
                    {
                        DBHelper.CmdScalar($"UPDATE [dbo].[Employee] SET [Surname] = '{FirstToUpper(surnameBox.Text)}', [Name] = '{FirstToUpper(nameBox.Text)}', [MiddleName] = '{FirstToUpper(middlenameBox.Text)}', [Description] = '{descriptionBox.Text}' WHERE [ID_Employee] = {dataSet.Tables[0].Rows[0].ItemArray[0]}");
                    }
                    else
                    {
                        if (!checkBox1.Checked && photoExist)
                        {
                            DBHelper.CmdScalar($"UPDATE [dbo].[Employee] SET [Surname] = '{FirstToUpper(surnameBox.Text)}', [Name] = '{FirstToUpper(nameBox.Text)}', [MiddleName] = '{FirstToUpper(middlenameBox.Text)}', [Image_ID] = NULL, [Description] = '{descriptionBox.Text}' WHERE [ID_Employee] = {dataSet.Tables[0].Rows[0].ItemArray[0]}");
                            DBHelper.CmdScalar($"DELETE FROM [dbo].[Images] WHERE [ID_Image] = {dataSet.Tables[0].Rows[0].ItemArray[6]}");
                        }
                        else
                        {
                            if (photoExist && checkBox1.Checked && photoChanged)
                            {
                                sqlCommand = new SqlCommand();
                                sqlCommand.CommandText = $@"UPDATE [dbo].[Images] SET [Image] = @Image, [Format] = @Format WHERE [ID_Image] = {dataSet.Tables[0].Rows[0].ItemArray[6]}";
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
                                DBHelper.CmdScalar($"UPDATE [dbo].[Employee] SET [Surname] = '{FirstToUpper(surnameBox.Text)}', [Name] = '{FirstToUpper(nameBox.Text)}', [MiddleName] = '{FirstToUpper(middlenameBox.Text)}', [Description] = '{descriptionBox.Text}' WHERE [ID_Employee] = {dataSet.Tables[0].Rows[0].ItemArray[0]}");
                            }
                            else DBHelper.CmdScalar($"UPDATE [dbo].[Employee] SET [Surname] = '{FirstToUpper(surnameBox.Text)}', [Name] = '{FirstToUpper(nameBox.Text)}', [MiddleName] = '{FirstToUpper(middlenameBox.Text)}', [Description] = '{descriptionBox.Text}' WHERE [ID_Employee] = {dataSet.Tables[0].Rows[0].ItemArray[0]}");
                        }
                    }
                }
                FormAdd form = Owner as FormAdd;
                form.showGridView.DataSource = DBHelper.FillDataSet("SELECT * FROM [dbo].[Employee_show]").Tables[0];
                form.showGridView.Columns[0].Visible = false;
                Close();
            }
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                photoButton.Enabled = true;
                pictureBox1.Enabled = true;
            }
            else
            {
                photoButton.Enabled = false;
                pictureBox1.Enabled = false;
            }
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
