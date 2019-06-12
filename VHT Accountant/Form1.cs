using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VHT_Accountant
{
    public partial class Form1 : Form
    {
        string searchID = "";
        bool inOrOut = false; //true = out, false = in
        string info = "";
        decimal price = 0.0m;
        string whom = "";
        string category = "";
        string note = "";
        bool moneyBackYet = false; //true = got it, false = not yet
        DateTime insertDate;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
                 try
            {
                insertIntoDB("INSERT INTO MoneyIO(Name,Gender) values ('alex','male')");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }

        private void insertIntoDB(string query)
        {
            string createQuery = "";
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=.\\VHTDatabase.db"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = createQuery;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO MoneyIO(Name,Gender) values ('alex','male')";
                    cmd.ExecuteNonQuery();
                    conn.Close();

            //        cmd.CommandText = "Select * from Mytable";
            //        using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                Console.WriteLine(reader["name"] + " " + reader["gender"]);
            //            }
            //            conn.Close();
            //        }
            //    }
            }
            ////Console.ReadLine();
        }
    }
}
