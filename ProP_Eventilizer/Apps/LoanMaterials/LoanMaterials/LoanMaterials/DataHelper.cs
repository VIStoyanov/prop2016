using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanMaterials
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

        public List<Item> GetInfoAboutLoanMaterials(List<Item> currentlist)
        {
            String sql = "SELECT * FROM `equipment`;"; 
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<Item> temp;
            temp = new List<Item>();


            try
            {
                if (currentlist.Count == 0)
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    int equipmentId, price, inStock;
                    string name;
                    while (reader.Read())
                    {
                        equipmentId = Convert.ToInt32(reader["Equipment_id"]);
                        name = Convert.ToString(reader["Description"]);
                        price = Convert.ToInt32(reader["Price"]);
                        inStock = Convert.ToInt32(reader["InStock"]);
                        temp.Add(new Item(equipmentId, name, price, inStock, 0));


                    }
                }
                else
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    int equipmentId, price, inStock;
                    string name;
                    while (reader.Read())
                    {
                        equipmentId = Convert.ToInt32(reader["Equipment_id"]);
                        name = Convert.ToString(reader["Description"]);
                        price = Convert.ToInt32(reader["Price"]);
                        inStock = Convert.ToInt32(reader["InStock"]);
                        temp.Add(new Item(equipmentId, name, price, inStock, currentlist[equipmentId-1].Quantity));


                    }
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

      

        public bool MakeRent(int clientid,List<Item> items)
        {
            bool successful = false;

            MySqlCommand command = connection.CreateCommand();
            try
            {
                connection.Open();
                command.CommandText = "SET autocommit=0;";
                command.ExecuteNonQuery();

                command.CommandText = "LOCK TABLES rent WRITE,rent as r2 READ,rent_equipment WRITE,equipment WRITE;";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO rent (Rent_id,Client_Client_id1)VALUES( (SELECT IFNULL(MAX(Rent_id),0) from rent r2) + 1," + clientid + ");";
                command.ExecuteNonQuery();

                foreach (var item in items)
                {
                    if (item.Quantity > 0)
                    {
                        this.GenerateSqlCodeForEquipment(item.EquipmentId, item.Quantity, command);
                        item.Quantity = 0;
                    }
                }

                command.CommandText = "COMMIT;";
                command.ExecuteNonQuery();

                command.CommandText = "UNLOCK TABLES;";
                command.ExecuteNonQuery();
                successful = true;

            }
            catch
            {
                command.CommandText = "ROLLBACK;";
                command.ExecuteNonQuery();
                command.CommandText = "UNLOCK TABLES;";
                command.ExecuteNonQuery(); 
            }

            finally
            {
                connection.Close();
            }
            return successful;
        }

        private void GenerateSqlCodeForEquipment(int equipid,int amount, MySqlCommand command)
        {
            command.CommandText = " INSERT INTO rent_equipment (Equipment_Equipment_id,Rent_Rent_id,Amount) VALUES(" + equipid + ",(SELECT IFNULL(MAX(Rent_id),0) from rent r2)," + amount + ")";
            command.ExecuteNonQuery();
            command.CommandText = " UPDATE equipment SET equipment.InStock = equipment.InStock - " + amount + " WHERE equipment.Equipment_id =" + equipid + ";";
            command.ExecuteNonQuery();
        }




        public int CheckQuantity()
        {
            String sql = "SELECT IFNULL(SUM(equipment.InStock),0) AS TOTALINSTOCK from equipment;";
            MySqlCommand command = new MySqlCommand(sql, connection);

            int quantity = -1;

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    quantity = Convert.ToInt32(reader["TOTALINSTOCK"]);
                }


            }
            catch
            {
                MessageBox.Show("error while loading data");
            }
            finally
            {
                connection.Close();
            }
            return quantity;
        }

        public void CheckIfThereHaveBeenChanges(int quantity)
        {
            int latestquantity = this.CheckQuantity();

            if (latestquantity!=quantity)
            {
                if (EventDataChanged != null)
                {
                    EventDataChanged();
                }
            }

        }





    }
}
