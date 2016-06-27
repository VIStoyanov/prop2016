using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtTheEntranceV2
{
    class DataHelper
    {
        public MySqlConnection connection;

        public DataHelper()
        {

            String connectionInfo = "server=localhost;user id=root;password=;database=mydb;";

            connection = new MySqlConnection(connectionInfo);

        }

        public List<string> GetClient(int barcodenumber)
        {
            string sql = "SELECT * FROM client WHERE client.barcode = " + barcodenumber + ";";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<string> data;
            data = new List<string>();

          


            try
            {
                string id,firstname,lastname,email;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = Convert.ToString(reader["Client_id"]);
                    firstname = Convert.ToString(reader["First_name"]);
                    lastname = Convert.ToString(reader["Last_name"]);
                    email = Convert.ToString(reader["e-mail"]);

                    data.Add(id);
                    data.Add(firstname);
                    data.Add(lastname);
                    data.Add(email);
                }

            }
            catch
            {
                MessageBox.Show("Error while processing request.");
            }
            finally
            {
                connection.Close();
            }
            return data;
        }
        public void UpdateRFIDAndChangeStatus(int id, string rfid)
        {
            String sql = "UPDATE client SET client.RFID = '" + rfid + "' WHERE client.Client_id = " + id + "";

            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlTransaction trans = null;
            

            
            try
            {
               
                connection.Open();
                trans = connection.BeginTransaction();
                command.Transaction = trans;

                command.ExecuteNonQuery();
                command.CommandText = "Update `check` SET `check`.Type = 'PRESENT' WHERE `check`.Client_Client_id = "+id+";";
                command.ExecuteNonQuery();
                trans.Commit();
                MessageBox.Show("Successfully updated RFID.");

            }
            catch
            {
                if(trans!=null)
                { trans.Rollback(); }
                
                MessageBox.Show("Error while processing request.");
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
