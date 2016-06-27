using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace VisitorLeavesTheEvent
{
    class DataHelper
    {
        public MySqlConnection connection;

        public DataHelper()
        {

            String connectionInfo = "server=localhost;user id=root;password=;database=mydb;";

            connection = new MySqlConnection(connectionInfo);

        }


        public List<string> GetInfoAboutPerson(string rfid)
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
                MessageBox.Show("error while loading the people");
            }
            finally
            {
                connection.Close();
            }
            return temp;

        }
        public int CheckIfPersonHasToReturn(int personid)
        {
            String sql = "SELECT COUNT(*) FROM RENT WHERE Client_Client_id1 ="+personid+" AND rent.TimeOfReturn IS NULL;";
            MySqlCommand command = new MySqlCommand(sql, connection);
            int result = 0;
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
      
                while (reader.Read())
                {
                    result = Convert.ToInt16(reader["COUNT(*)"]);
                }
            }
            catch
            {
                MessageBox.Show("error while loading the people");
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public bool UnassignRFIDAndChangeStatus(int personid)
        {
            String sql = "UPDATE `client` SET `RFID` = '' WHERE `client`.`Client_id` = " + personid + ";";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlTransaction trans = null;
            bool success = false;
          
            try
            {
                connection.Open();
                trans = connection.BeginTransaction();
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.CommandText = "Update `check` SET `Type` = 'LEFT' WHERE Client_Client_id = " + personid + ";";
                command.ExecuteNonQuery();
                trans.Commit();
                success = true;
                
            }
            catch
            {
                if(trans != null)
                {
                    trans.Rollback();
                }
                MessageBox.Show("Error while processing sql statement.");
            }
            finally
            {
                connection.Close();
            }
            return success;
           
        }


    }
}
