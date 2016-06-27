using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatusOfEvent
{
    class DataHelper
    {
        public MySqlConnection connection;

        public DataHelper()
        {

            String connectionInfo = "server=localhost;user id=root;password=;database=mydb;";

            connection = new MySqlConnection(connectionInfo);

        }
        public List<string> GetAllNames()
        {
            String sql = "SELECT * FROM client";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<string> temp;
            temp = new List<string>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                String firstname;
                String lastname;


                while (reader.Read())
                {
                    firstname = Convert.ToString(reader["First_name"]);
                    lastname = Convert.ToString(reader["Last_name"]);

                    temp.Add(firstname);
                    temp.Add(lastname);
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
        public List<string> GetAllArticles()
        {
            String sql = "SELECT * FROM products";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<string> temp;
            temp = new List<string>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string productdesc, productid;


                while (reader.Read())
                {
                    productid = Convert.ToString(reader["Product_id"]);
                    productdesc = Convert.ToString(reader["Product_desc"]);

                    temp.Add(productid);
                    temp.Add(productdesc);
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
        public List<string> GetAllShops()
        {
            String sql = "SELECT * FROM shop;";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<string> temp;
            temp = new List<string>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string shopid, shopname;


                while (reader.Read())
                {
                    shopid = Convert.ToString(reader["Shop_id"]);
                    shopname = Convert.ToString(reader["Shop_name"]);

                    temp.Add(shopid);
                    temp.Add(shopname);
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

        public int GetSoldForOneArticle(string id)
        {
            String sql = 
                "SELECT IFNULL(SUM(products_purchase.Quantity),0) AS ARTICLESSOLD,products.Product_id" +
                " FROM products LEFT JOIN products_purchase ON (products.Product_id = products_purchase.Products_Product_id)" + 
                " WHERE products.Product_id = '" + id + "' GROUP BY Product_id;";
            MySqlCommand command = new MySqlCommand(sql, connection);

            int articlessold = 0;
            
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                



                
                while (reader.Read())
                {
                    
                    articlessold = Convert.ToInt32(reader["ARTICLESSOLD"]);
                    

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
            return articlessold;
        }
        public int GetAmountOfMoneySoldForAShop(string id)
        {
            String sql = 
                "SELECT IFNULL(SUM(products.Price* products_purchase.Quantity),0)AS INCOME,shop.Shop_id " +
                " FROM products JOIN products_purchase ON (products.Product_id = products_purchase.Products_Product_id)"+
                " JOIN purchase on (products_purchase.Purchase_Purchase_id = purchase.Purchase_id) JOIN shop ON (purchase.Shop_Shop_id = shop.Shop_id)"+
                " JOIN client ON (purchase.Client_Client_id = client.Client_id) WHERE shop.Shop_id = '" + id + "';";
            MySqlCommand command = new MySqlCommand(sql, connection);

            int income = 0;

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();



                
                while (reader.Read())
                {
                    
                    income = Convert.ToInt32(reader["INCOME"]);
                    

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
            return income;
        }

        public List<string> GetStatusOfPerson(string firstname,string lastname)
        {
            String sql = "SELECT `check`.type,(SELECT IFNULL(SUM(`transaction`.Amount),0) FROM `transaction` JOIN `client` ON (`client`.`Client_id` = `transaction`.`Client_Client_id`)WHERE client.First_name = '" + firstname + "' AND client.Last_name = '" + lastname + "') AS TRANSFEREDMONEY"+
                         " , (SELECT IFNULL(booking.Price,0) FROM booking JOIN client " +
                         " ON (booking.Client_Client_id = client.Client_id)  WHERE client.First_name = '" + firstname + "' AND client.Last_name = '" + lastname + "') AS TICKETPRICE," +
                         " (SELECT IFNULL(SUM(products.Price* products_purchase.Quantity),0)" +
                         " FROM products JOIN products_purchase ON (products.Product_id = products_purchase.Products_Product_id)" +
                         " JOIN purchase on (products_purchase.Purchase_Purchase_id = purchase.Purchase_id)"+
                         " JOIN shop ON (purchase.Shop_Shop_id = shop.Shop_id)"+
                         " JOIN client ON (purchase.Client_Client_id = client.Client_id) WHERE client.First_name = '" + firstname + "' AND client.Last_name = '" + lastname + "') AS MONEYSPENTATSHOPS,"+
                         "(SELECT client.Balance FROM client WHERE client.First_name = '"+firstname+"' AND client.Last_name = '"+lastname+"') AS BALANCE FROM" +
                         " `check` JOIN client ON (`check`.`Client_Client_id` = client.Client_id) WHERE client.First_name = '" + firstname + "' AND client.Last_name = '" + lastname + "';";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<string> temp;
            temp = new List<string>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();



                string type, moneyspentinshops, ticketprice, transferedmoney,balance;
                while (reader.Read())
                {
                    type = Convert.ToString(reader["Type"]);
                    moneyspentinshops = Convert.ToString(reader["MONEYSPENTATSHOPS"]);
                    ticketprice = Convert.ToString(reader["TICKETPRICE"]);
                    transferedmoney = Convert.ToString(reader["TRANSFEREDMONEY"]);
                    balance = Convert.ToString(reader["BALANCE"]);
                    temp.Add(type);
                    temp.Add(moneyspentinshops);
                    temp.Add(ticketprice);
                    temp.Add(transferedmoney);
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
        public List<string> GetStatusOfPeople()
        {

            String sql = 
                " SELECT COUNT(*) AS 'PRESENT',(SELECT COUNT(*) FROM `check` WHERE `check`.Type = 'TOARRIVE') AS 'TOARRIVE'," +
                "(SELECT COUNT(*) FROM `check` WHERE `check`.Type = 'LEFT') AS 'LEFT' FROM `check` WHERE `check`.Type = 'PRESENT'";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<string> temp;
            temp = new List<string>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();



                string present, toarrive, left;
                while (reader.Read())
                {
                    present = Convert.ToString(reader["PRESENT"]);
                    toarrive = Convert.ToString(reader["TOARRIVE"]);
                    left = Convert.ToString(reader["LEFT"]);
                    temp.Add(present);
                    temp.Add(toarrive);
                    temp.Add(left);

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

        public List<string> GetInfoAboutAllAccounts()
        {
            String sql = //Total money spent
                " SELECT (IFNULL(SUM(products.Price* products_purchase.Quantity),0) + " + "(SELECT IFNULL(SUM(booking.Price),0) FROM booking) +"+
                " (SELECT ((SELECT COUNT(*) FROM CLIENT JOIN tent ON (client.Client_id = tent.Client_Client_id))*30+"+
                "          (Count(c1.Client_id))*20) AS TotalSpentonTents FROM client c1, client c2 WHERE c2.Client_id = c1.Client_Client_id) + "+
                "          (SELECT IFNULL(SUM( ( (CASE WHEN(EXTRACT(HOUR FROM rent.TimeOfReturn) - EXTRACT(HOUR FROM rent.TimeOfRent))=0 THEN 1 ELSE (EXTRACT(HOUR FROM rent.TimeOfReturn) - EXTRACT(HOUR FROM rent.TimeOfRent)) END) +DATEDIFF(rent.TimeOfReturn,rent.TimeOfRent)*24)*" +
                "          ( (rent_equipment.Amount) * (equipment.Price))),0) FROM rent JOIN rent_equipment ON (rent.Rent_id = rent_equipment.Rent_Rent_id) "+
                "          JOIN equipment ON (rent_equipment.Equipment_Equipment_id = equipment.Equipment_id))) AS TOTALMONEYSPENT,"+  

                //Total Balance
                " (SELECT IFNULL(SUM(client.Balance),0)AS BALANCE FROM client)AS BALANCEOFALLACCOUNTS,"+

                //Visitors that booked a spot
                " (SELECT COUNT(tent.Client_Client_id) FROM tent)AS VISITORSBOOKEDASPOT,"+

                //Total money spent on tents
                " (SELECT ((SELECT COUNT(*) FROM CLIENT JOIN tent ON (client.Client_id = tent.Client_Client_id))*30+(Count(c1.Client_id))*20)"+
                "  AS TotalSpentonTents FROM client c1, client c2 WHERE c2.Client_id = c1.Client_Client_id)AS MONEYSPENTOTTENTS"+


                " FROM products JOIN products_purchase ON (products.Product_id = products_purchase.Products_Product_id)"+
                " JOIN purchase on (products_purchase.Purchase_Purchase_id = purchase.Purchase_id) JOIN shop ON (purchase.Shop_Shop_id = shop.Shop_id)"+
                " JOIN client ON (purchase.Client_Client_id = client.Client_id);";
            MySqlCommand command = new MySqlCommand(sql, connection);
           
            List<string> temp;
            temp = new List<string>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();



                string totalmoneyspent, balanceofallaccounts, visitorsbookedaspot,moneyspentontents;
                while (reader.Read())
                {
                    totalmoneyspent = Convert.ToString(reader["TOTALMONEYSPENT"]);
                    balanceofallaccounts = Convert.ToString(reader["BALANCEOFALLACCOUNTS"]);
                    visitorsbookedaspot = Convert.ToString(reader["VISITORSBOOKEDASPOT"]);
                    moneyspentontents = Convert.ToString(reader["MONEYSPENTOTTENTS"]);
                    temp.Add(totalmoneyspent);
                    temp.Add(balanceofallaccounts);
                    temp.Add(visitorsbookedaspot);
                    temp.Add(moneyspentontents);
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

        public List<string> GetInfoAboutFreeCampingSpots()
        {
             String sql = "SELECT IFNULL(tent.Client_Client_id,'FREE') AS FREESPOTS,Tent_spot_id from tent;"; 
             MySqlCommand command = new MySqlCommand(sql, connection);

             List<string> temp;
             temp = new List<string>();

             try
             {
                 connection.Open();
                 MySqlDataReader reader = command.ExecuteReader();



                 string freeSpots, TentSpotId;
                 while (reader.Read())
                 {
                     freeSpots = Convert.ToString(reader["FREESPOTS"]);
                     TentSpotId = Convert.ToString(reader["Tent_spot_id"]);
                     temp.Add(freeSpots);
                     temp.Add(TentSpotId);
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
