using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CampingEntrance
{
    class dataHelper
    {
        public MySqlConnection connection;

        public dataHelper()
        {

            String connectionInfo = "server=localhost;user id=root;password=;database=mydb;";

            connection = new MySqlConnection(connectionInfo);

        }
        public Person GetInfoAboutPerson(string rfid)
        {
            String sql = "SELECT Client_id,First_name,Last_name,IFNULL(Client_Client_id,0) as HOST FROM client WHERE client.RFID = '" + rfid + "'";
            MySqlCommand command = new MySqlCommand(sql, connection);

            Person person = null;


            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                string firstname, lastname;
                int clientid, hostid;
                while (reader.Read())
                {
                    clientid = Convert.ToInt16(reader["Client_id"]);
                    firstname = Convert.ToString(reader["First_name"]);
                    lastname = Convert.ToString(reader["Last_name"]);
                    hostid = Convert.ToInt16(reader["HOST"]);
                    person = new Person(clientid, firstname, lastname, hostid);

                } 
            }
            catch
            {
                MessageBox.Show("Error while loading the person.");
            }
            finally
            {
                connection.Close();
            }
            return person;

        }

        public List<Group> GetTheHostAndGuest()
        {
            List<Group> temp = new List<Group>();


            String sql = "SELECT tent.Tent_spot_id as tentId, client.Client_id as hostid, client.First_name as hostfirstname,client.Last_name as hostlastname, IFNULL(c1.Client_id,-1) as guestid, IFNULL(c1.First_name,-1) as guestfirstname, IFNULL(c1.Last_name,-1) as guestlastname FROM `tent` JOIN client ON (tent.Client_Client_id = client.Client_id) LEFT JOIN client c1 ON (c1.Client_Client_id = client.Client_id);";
             MySqlCommand command = new MySqlCommand(sql, connection);

             try
             {
                 connection.Open();
                 MySqlDataReader reader = command.ExecuteReader();
                 int tentId, hostId = -1, guestId;
                 string hostFirstName, hostLastName, guestFirstName, guestLastName;
                 int oldhostid;
                 Group newObj = null;


                 while (reader.Read())
                 {
                     oldhostid = hostId;

                     tentId = Convert.ToInt16(reader["tentId"]);

                     hostId = Convert.ToInt16(reader["hostid"]);
                     hostFirstName = Convert.ToString(reader["hostfirstname"]);
                     hostLastName = Convert.ToString(reader["hostlastname"]);

                     guestId = Convert.ToInt16(reader["guestid"]);
                     guestFirstName = Convert.ToString(reader["guestfirstname"]);
                     guestLastName = Convert.ToString(reader["guestlastname"]);

                     Person host = new Person(hostId, hostFirstName, hostLastName, 0);
                     Person guest = new Person(guestId, guestFirstName, guestLastName, host.HostId);
                     if (oldhostid==-1|| oldhostid != hostId)
                     {

                         
                         newObj = new Group(tentId, host);
                         if (guest.Id != -1)
                         {
                             newObj.InsertIntoGuests(guest);
                         }
                         temp.Add(newObj);
                     }
                     else
                     {
                         if (guest.Id!=-1)
                         {
                             newObj.InsertIntoGuests(guest);
                         }
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




    }
}
