using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace AlisavetRating
{
    public partial class Form1 : Form
    {
        private static string path = $"{Environment.CurrentDirectory}//ip.txt";
        private static StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
        private static string ip = (sr.ReadLine());
        private static int port = 5000;
        private static string pathFilial = $"{Environment.CurrentDirectory}//filial.txt";
        private static StreamReader reader = new StreamReader(pathFilial, System.Text.Encoding.Default);
        private string filial = (reader.ReadLine());
        private bool cancel = true;
        private Thread thrServer;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            showGridView.DataSource = DBHelper.FillDataSet("SELECT * FROM Employee_show").Tables[0];
            showGridView.Columns[0].Visible = false;
        }
        
        private void findBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(findBox.Text))
            {
                showGridView.DataSource = DBHelper.FillDataSet($"SELECT * FROM Employee_show WHERE Фамилия like '%{findBox.Text}%' or Имя like '%{findBox.Text}%' or Отчество like '%{findBox.Text}%' or" +
                    $" Фамилия + ' ' + Имя like '%{findBox.Text}%' or Имя + ' ' + Фамилия like '%{findBox.Text}%' or Имя + ' ' + Отчество like '%{findBox.Text}%' " +
                    $"or Фамилия + ' ' + Имя + ' ' + Отчество like '%{findBox.Text}%' or Имя + ' ' + Отчество + ' ' + Фамилия like '%{findBox.Text}%'").Tables[0];
                showGridView.Columns[0].Visible = false;
            }
            else
            {
                showGridView.DataSource = DBHelper.FillDataSet("SELECT * FROM Employee_show").Tables[0];
                showGridView.Columns[0].Visible = false;
            }
        }

        private void findBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(findBox.Text))
                {
                    showGridView.DataSource = DBHelper.FillDataSet($"SELECT * FROM Employee_show WHERE Фамилия like '%{findBox.Text}%'").Tables[0];
                    showGridView.Columns[0].Visible = false;
                }
                else
                {
                    showGridView.DataSource = DBHelper.FillDataSet("SELECT * FROM Employee_show").Tables[0];
                    showGridView.Columns[0].Visible = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showGridView.DataSource = DBHelper.FillDataSet("SELECT * FROM Employee_show").Tables[0];
            showGridView.Columns[0].Visible = false;
            findBox.Text = "";
        }

        private void showRateBtn_Click(object sender, EventArgs e)
        {
            if (showGridView.SelectedRows.Count != 0)
            {
                thrServer = new Thread(new ParameterizedThreadStart(serverStartListening));
                thrServer.IsBackground = true;
                thrServer.Start();
            }
        }

        private void serverStartListening(object sender)
        {
            showRateBtn.Invoke((MethodInvoker)delegate
            {
                showRateBtn.Text = "Отправка...";
                showRateBtn.Enabled = false;
            });
            //MessageBox.Show($"{showGridView.SelectedRows[0].Cells[2].Value.ToString()} {showGridView.SelectedRows[0].Cells[1].Value.ToString()}");
            string finalRate = null;
            var tspEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tspSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tspSocket.Bind(tspEndPoint);
            tspSocket.Listen(5);
            ImageConverter _imageConverter = new ImageConverter();
            byte[] pic = new byte[1];
            if (DBHelper.CmdScalar($"SELECT Image_ID FROM Employee WHERE ID_Employee = {showGridView.SelectedRows[0].Cells[0].Value}") != "") pic = (byte[])_imageConverter.ConvertTo(DBHelper.CmdImg($"SELECT Image FROM Images WHERE ID_Image = {DBHelper.CmdScalar($"SELECT Image_ID FROM Employee WHERE ID_Employee = {showGridView.SelectedRows[0].Cells[0].Value}")}"), typeof(byte[]));
            // MessageBox.Show(pic.Length.ToString());
            int countBytes = 0;
            int saveCountBytes = 0;
            while (cancel)
            {
                var listener = tspSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (listener.Available > 0 && cancel);
                if ($"{data}" == "Connect")
                {
                    listener.Send(Encoding.UTF8.GetBytes("Connected"));
                }

                if ($"{data}" == "GO")
                {
                    string sizeImage = pic.Length.ToString();
                    for (int i = pic.Length.ToString().Length; i < 10; i++)
                    {
                        sizeImage = "0" + sizeImage;
                    }
                    listener.Send(Encoding.UTF8.GetBytes(sizeImage));
                }

                int muchBytes = 1024;

                if ($"{data}" == "IMAGE")
                {
                    byte[] part = new byte[muchBytes];

                    for (int i = 0; i < muchBytes && countBytes < pic.Length; i++)
                    {
                        part[i] = pic[countBytes];
                        countBytes++;
                    }

                    listener.Send(part);
                }
                else if ($"{data}" == "ERRORIMAGE")
                {
                    countBytes = saveCountBytes;

                    byte[] part = new byte[muchBytes];

                    for (int i = 0; i < muchBytes && countBytes < pic.Length; i++)
                    {
                        part[i] = pic[countBytes];
                        countBytes++;
                    }

                    listener.Send(part);
                }
                else if ($"{data}" == "getText")
                {
                    listener.Send(Encoding.UTF8.GetBytes($"{showGridView.SelectedRows[0].Cells[2].Value.ToString()} {showGridView.SelectedRows[0].Cells[1].Value.ToString()}"));
                    showRateBtn.Invoke((MethodInvoker)delegate
                    {
                        showRateBtn.Text = "Оценивание...";
                        showRateBtn.BackColor = Color.DarkCyan;
                    });
                }
                switch ($"{data}")
                {
                    case "0.0":
                        finalRate = $"{data}";
                        break;
                    case "1.0":
                        finalRate = $"{data}";
                        break;
                    case "2.0":
                        finalRate = $"{data}";
                        break;
                    case "3.0":
                        finalRate = $"{data}";
                        break;
                    case "4.0":
                        finalRate = $"{data}";
                        break;
                    case "5.0":
                        finalRate = $"{data}";
                        break;
                    case "6.0":
                        finalRate = $"{data}";
                        break;
                    case "7.0":
                        finalRate = $"{data}";
                        break;
                    case "8.0":
                        finalRate = $"{data}";
                        break;
                    case "9.0":
                        finalRate = $"{data}";
                        break;
                    case "10.0":
                        finalRate = $"{data}";
                        break;
                }
                if (finalRate != null)
                {
                    if (finalRate != "0.0")
                    {
                        DBHelper.CmdScalar($"INSERT INTO [dbo].[Rates] (Employee_ID, Branch_ID, ReceiptDate, Rate) VALUES ({showGridView.SelectedRows[0].Cells[0].Value.ToString()}, {filial}, '{DateTime.Now.ToString()}', {finalRate})");
                    }
                    data = null;
                    listener.Send(Encoding.UTF8.GetBytes("Received"));
                    finalRate = null;
                    listener.Shutdown(SocketShutdown.Both);
                    listener.Close();
                    break;
                }
                saveCountBytes = countBytes;
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();

            }
            tspSocket.Close();
            this.showRateBtn.Invoke((MethodInvoker)delegate 
            {
                this.showRateBtn.Text = "Вывести на оценивающее устройство";
                showRateBtn.BackColor = DefaultBackColor;
                showRateBtn.Enabled = true;
            });
        }
    }
}