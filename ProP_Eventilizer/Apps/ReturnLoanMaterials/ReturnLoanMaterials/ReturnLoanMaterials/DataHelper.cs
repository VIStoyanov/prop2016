using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ReturnLoanMaterials
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

        public List<Rent> GetInfoAboutLoanMaterials(int clientId)
        {
            String sql = "SELECT rent_equipment.Equipment_Equipment_id AS EQUIPID,rent_equipment.Amount,equipment.Price,equipment.Description,rent.Rent_id,rent.TimeOfRent,rent.TimeOfReturn,rent.Client_Client_id1 from rent_equipment join rent ON (rent_equipment.Rent_Rent_id = rent.Rent_id) JOIN equipment ON (rent_equipment.Equipment_Equipment_id = equipment.Equipment_id) WHERE rent.TimeOfReturn IS NULL AND rent.Client_Client_id1 = " + clientId + ";";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<Rent> temp = new List<Rent>();
            Rent rent = null;
            
           


            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                int equipmentId, price, amount,rentid = -1;
                DateTime timeofrent;
                string name;
                while (reader.Read())
                {
                    int oldrent = rentid;
                    equipmentId = Convert.ToInt32(reader["EQUIPID"]);
                    name = Convert.ToString(reader["Description"]);
                    price = Convert.ToInt32(reader["Price"]);
                    amount = Convert.ToInt32(reader["Amount"]);
                    rentid = Convert.ToInt32(reader["Rent_id"]);
                    string strtimeofrent = Convert.ToString(reader["TimeOfRent"]);
                    timeofrent = Convert.ToDateTime(strtimeofrent);

                   
                    Item i = new Item(equipmentId, name, price, amount);
                    if (oldrent != rentid)
                    {
                        rent = new Rent(rentid, timeofrent);
                        rent.InsertItemintoRent(i);
                        temp.Add(rent);
                    }
                    else
                    {
                        rent.InsertItemintoRent(i);
                    }

                }
            }
         
            finally
            {
                connection.Close();
            }
            return temp;

        }

        public bool ReturnRent(Rent rent,int clientid,decimal price)
        {

            MySqlCommand command = connection.CreateCommand();
            bool success = false;
            try
            {
                connection.Open();
                
                command.CommandText = "SET autocommit=0;";
                command.ExecuteNonQuery();
                command.CommandText = "LOCK TABLES rent WRITE,client WRITE,equipment WRITE";
                command.ExecuteNonQuery();
                DateTime timeofreturn = DateTime.Now;
                command.CommandText = "UPDATE rent SET rent.TimeOfReturn = '" + timeofreturn.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE rent.Rent_id = " + rent.RentId + ";";
                command.ExecuteNonQuery();
                foreach(var item in rent.rentedItems)
                {
                    command.CommandText = GenerateSQLCodeForItem(item);
                    command.ExecuteNonQuery();
                }
                command.CommandText = "UPDATE client SET client.Balance = Balance - " + price + " WHERE client.Client_id = " + clientid + ";";
                command.ExecuteNonQuery();
                command.CommandText = "COMMIT;";
                command.ExecuteNonQuery();
                command.CommandText = "UNLOCK TABLES;";
                command.ExecuteNonQuery();
                success = true;
                
            }
            catch
            {
                command.CommandText = "ROLLBACK;";
                command.ExecuteNonQuery();
                command.CommandText = "UNLOCK TABLES;";
                command.ExecuteNonQuery();
                MessageBox.Show("Error processing the statement.");
            }

            finally
            {
                connection.Close();
            }
            return success;
        }

        private string GenerateSQLCodeForItem (Item i)
        {
            return "UPDATE equipment SET equipment.InStock = " + i.Quantity + " + equipment.InStock WHERE equipment.Equipment_id = "+i.EquipmentId+";";
        }

        public string ReturnDateTimeAsString(int rentId)
        {
            return "hello";
        }
    }
}
