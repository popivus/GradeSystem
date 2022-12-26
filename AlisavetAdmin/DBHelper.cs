using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlisavetAdmin
{
    static class DBHelper
    {
        private static string path = $"{Environment.CurrentDirectory}//connectionstringDB.txt";
        private static StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
        private static string ip = (sr.ReadLine());
        public static string connectionString = $@"Data Source={ip};Initial Catalog=alisavetRating; Integrated Security=false;User ID=sa;Password=5Dfg893ass";
        private static SqlConnection connection;
        private static SqlCommand cmd;
        private static DataSet dataSet;
        private static SqlDataAdapter dataAdapter;
        public static string CmdScalar(string command)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                cmd = new SqlCommand(command, connection);
                cmd.ExecuteNonQuery();
                if (!command.StartsWith("INSERT"))
                {
                    if (cmd.ExecuteScalar() != null) return cmd.ExecuteScalar().ToString();
                    else return null;
                }
                else return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        public static Image CmdImg(string command)
        {
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                cmd = new SqlCommand(command, connection);
                MemoryStream stream = new MemoryStream((byte[])cmd.ExecuteScalar());
                return Image.FromStream(stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Заполняет DataSet данными из запроса
        /// </summary>
        /// <param name="command">Запрос</param>
        /// <param name="dataSet">Заполняемый DataSet</param>
        public static DataSet FillDataSet(string command)
        {
            dataSet = new DataSet();
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                dataAdapter = new SqlDataAdapter(command, connection);
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
