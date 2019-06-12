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
using System.IO;
namespace VHT_Accountant
{
    public partial class Form1 : Form
    {
        string searchID = "";
        int inOrOut = 0; //true = out, false = in
        string info = "";
        double price = 0.00;
        string whom = "";
        string category = "";
        string note = "";
        int moneyBackYet = 0; //true = got it, false = not yet
        string insertDate;
        public Form1()
        {
            InitializeComponent();
            whomTxt.Items.Add("Benji");
            whomTxt.Items.Add("Chin Hei");
            catTxt.Items.Add("入貨-sampson");
            catTxt.Items.Add("入貨-netsea");
            catTxt.Items.Add("入貨-netmerci");
            catTxt.Items.Add("入貨-ssi");
            catTxt.Items.Add("入貨-wholesale");
            catTxt.Items.Add("payme");
            catTxt.Items.Add("paypal");
            catTxt.Items.Add("銀行");
            catTxt.Items.Add("轉數快");
            catTxt.Items.Add("");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //            MessageBox.Show(Path.GetDirectoryName(Application.ExecutablePath));

        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            searchID = serachTxt.Text;

            //   info = infoTxt.Text;
            price = Convert.ToDouble(priceTxt.Text);
            whom = whomTxt.Text;
            category = catTxt.Text;
            note = noteTxt.Text;
            if (moneyBackCheck.Checked == false) moneyBackYet = 0; else moneyBackYet = 1;//true = got it, false = not yet
            if (outBtn.Checked) inOrOut = 1; else inOrOut = 0; //true = out, false = in
            insertDate = dayPicker.SelectionRange.Start.ToShortDateString();
            try
            {
                insertIntoDB("INSERT INTO MoneyIO (inOrOut, Notes, Price, Whom, Category, MoneyBackYet, TimeOfIssue) values ( '" + inOrOut + "','" + note + "','" + price + "','" + whom + "','" + category + "','" + moneyBackYet + "','" + insertDate + "');");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
            clearInput();

        }

        private void insertIntoDB(string query)
        {
            string dataSrc = @"C:\Users\benji\Documents\Visual Studio 2013\Projects\VHT Accountant\VHT Accountant\VHTDatabase.db";
            // MessageBox.Show(dataSrc);
            //  string dataSrc = @"C:\Users\benji\Documents\Visual Studio 2013\Projects\VHT Accountant\VHT Accountant\VHTDatabase.db";
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=" + dataSrc))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();

                    Console.WriteLine(query);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    conn.Close();


                }
            }
        }

        private void clearInput()
        {
            priceTxt.Text = "";
            whomTxt.Text = "";
            catTxt.Text = "";
            noteTxt.Text = "";
            moneyBackCheck.Checked = false;
            outBtn.Checked = false;
            inBtn.Checked = false;
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            clearDataView();
        }

        private void clearDataView()
        {
            dataView.Rows.Clear();

        }
        private void searchAllBtn_Click(object sender, EventArgs e)
        {
            string searchTime = searchDate.SelectionRange.Start.ToShortDateString();
            searchDB(searchTime);
        }

        private void searchDB(string date)
        {
            clearDataView();
            string dataSrc = @"C:\Users\benji\Documents\Visual Studio 2013\Projects\VHT Accountant\VHT Accountant\VHTDatabase.db";
            string inOut = "";
            string moneyBack = "";
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=" + dataSrc))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    string query = "Select * from MoneyIO where TimeOfIssue = '" + date + "'";
                    conn.Open();
                    cmd.CommandText = query;
                    using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["InOrOut"].ToString() == "0")
                            {
                                inOut = "收入";
                            }
                            else
                            {
                                inOut = "支出";
                            }
                            if (reader["MoneyBackYet"].ToString() == "0")
                            {
                                moneyBack = "未拎返錢";
                            }
                            else
                            {
                                moneyBack = "已經拎返錢";
                            }
                            dataView.Rows.Add(inOut, reader["Notes"].ToString(), reader["Price"].ToString(), reader["Whom"].ToString(), reader["Category"].ToString(), moneyBack, reader["TimeOfIssue"].ToString());
                            // Console.WriteLine(reader["  "] + " " + reader["gender"]);
                        }
                        conn.Close();
                    }
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
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