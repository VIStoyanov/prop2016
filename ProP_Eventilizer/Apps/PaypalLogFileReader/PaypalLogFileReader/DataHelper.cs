using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaypalLogFileReader
{
    class DataHelper
    {
        public MySqlConnection connection;

        public delegate void DataChangedHandler();
        public event DataChangedHandler EventDataChanged;

        public DataHelper()
        {

            String connectionInfo = "server=localhost;user id=root;password=;database=mydb;";

            connection = new MySqlConnection(connectionInfo);

        }

        public List<string> FindPerson(string rfid)
        {
            String sql = "SELECT * FROM client WHERE client.RFID = '" + rfid + "'";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<string> temp;
            temp = new List<string>();


            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                string firstname, lastname, clientnumber, balance;
                while (reader.Read())
                {
                    clientnumber = Convert.ToString(reader["Client_id"]);
                    firstname = Convert.ToString(reader["First_name"]);
                    lastname = Convert.ToString(reader["Last_name"]);
                    balance = Convert.ToString(reader["Balance"]);
                    temp.Add(clientnumber);
                    temp.Add(firstname);
                    temp.Add(lastname);
                    temp.Add(balance);
                }
            }
            catch
            {
                MessageBox.Show("Error processing statement.");
            }
            finally
            {
                connection.Close();
            }
            return temp;
        }
        public void MakeListOfTransactions(StreamReader streamreader, int numofdeposits,DateTime date,ListBox lb)
        {
            MySqlCommand command = new MySqlCommand("", connection);
            MySqlTransaction trans = null;
            try
            {
                connection.Open();
                trans = connection.BeginTransaction();
                command.Transaction = trans;

                string depositid;
                double depositamount;
                string rfid;
                lb.Items.Add("Date: " + date.ToString("yyyy-MM-dd HH:mm:ss"));
              
                for (int i = 0; i < numofdeposits; i++)
                {
                    string depositdata = Convert.ToString(streamreader.ReadLine());

                    string[] datasplit = depositdata.Split(' ');
                    rfid = datasplit[0];
                    depositid = datasplit[1];
                    depositamount = Convert.ToDouble(datasplit[2]);

                    lb.Items.Add("RFID: " + rfid + " , Transaction ID: " + depositid + " , Amount: " + String.Format("{0:0.00}", depositamount));
                    this.InsertTransactionsForPerson(rfid, date, depositid, depositamount, command);
                }
                trans.Commit();
                MessageBox.Show("Inserting the transactions from the logfile was successful.");
            }
            catch (Exception ex)
            {
                if (trans != null)
                { trans.Rollback(); }
                lb.Items.Clear();
                MessageBox.Show(ex.Message);
               
            }
            finally
            {
                connection.Close();
            }
        }

        private void InsertTransactionsForPerson(string rfid,DateTime date,string transactionid,double amount, MySqlCommand command)
        {
            command.CommandText = "INSERT INTO transaction(transaction.Transaction_id,transaction.Client_Client_id,transaction.Amount,DateTime)"+
                                  " VALUES ('" + transactionid + "',(SELECT Client_id from client where client.RFID = '" + rfid + "')," + amount + ",'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "');";
            command.ExecuteNonQuery();
            command.CommandText = "UPDATE client SET client.Balance = "+amount+"+Balance WHERE client.RFID = '"+rfid+"';";
            command.ExecuteNonQuery();
        }

    }
}
