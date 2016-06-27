using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtTheShop
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

        public List<Item> GetAllProducts(List<Item> currentlist,int shopid)
        {
            String sql = "Select products.Product_id, products.Product_desc, products.Price,IFNULL(shops_product.Quantity,0) AS INSTOCKFORSHOP,"+
                " products.InStock,shops_product.Shop_Shop_id FROM products LEFT Join shops_product on "+
                " (products.Product_id = shops_product.Products_Product_id AND shops_product.Shop_Shop_id = "+shopid+");"; 
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<Item> temp;
            temp = new List<Item>();


            try
            {
                if (currentlist.Count == 0)
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    int productId, price, inStock, inStockTotal;
                    string description;
                    while (reader.Read())
                    {
                        productId = Convert.ToInt32(reader["Product_id"]);
                        description = Convert.ToString(reader["Product_desc"]);
                        price = Convert.ToInt32(reader["Price"]);
                        inStock = Convert.ToInt32(reader["INSTOCKFORSHOP"]);
                        inStockTotal = Convert.ToInt32(reader["InStock"]);

                        temp.Add(new Item(productId, description, price, inStock, inStockTotal, 0));


                    }
                }
                else
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    int productId, price, inStock, inStockTotal;
                    string description;
                    while (reader.Read())
                    {
                        productId = Convert.ToInt32(reader["Product_id"]);
                        description = Convert.ToString(reader["Product_desc"]);
                        price = Convert.ToInt32(reader["Price"]);
                        inStock = Convert.ToInt32(reader["INSTOCKFORSHOP"]);
                        inStockTotal = Convert.ToInt32(reader["InStock"]);

                        temp.Add(new Item(productId, description, price, inStock, inStockTotal, currentlist[productId-1].Quantity));


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

        public void InsertPurchase(int purchaseId, int clientId, int shopId)
        {
            String sql = "INSERT INTO purchase (Purchase_id,Client_Client_id, Shop_Shop_id)VALUES(" + purchaseId + "," + clientId + "," + shopId + ");";
            MySqlCommand command = new MySqlCommand(sql, connection);



            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

            }
            catch
            {
                MessageBox.Show("error while loading the people");
            }
            finally
            {
                connection.Close();
            }
        }

         public int ReturnLastPurchaseId()
        {
            String sql = " SELECT MAX(purchase.Purchase_id) AS LASTPURCHASEID from purchase;";
            MySqlCommand command = new MySqlCommand(sql, connection);
            int lastid = 0;


            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    lastid = Convert.ToInt32(reader["LASTPURCHASEID"]);
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
            return lastid;
        }

        public void InsertIntoProductsPurchase(int productId, int purchaseId, int quantity)
         {
             String sql = "INSERT INTO products_purchase(Products_Product_id, Purchase_Purchase_id, Quantity)VALUES(" + productId + "," + purchaseId + "," + quantity + ");";
             MySqlCommand command = new MySqlCommand(sql, connection);



             try
             {
                 connection.Open();
                 MySqlDataReader reader = command.ExecuteReader();

             }
             catch
             {
                 MessageBox.Show("error while loading the people");
             }
             finally
             {
                 connection.Close();
             }
         }

        public void UpdateProductsInstock(int productid, int newamount)
        {
            String sql = "UPDATE products SET products.InStock ="+newamount+" WHERE products.Product_id ="+productid+";";
            MySqlCommand command = new MySqlCommand(sql, connection);



            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

            }
            catch
            {
                MessageBox.Show("error while loading the people");
            }
            finally
            {
                connection.Close();
            }
        }
   

        public void UpdateProductsInstockForShop(int productid, int newamount, int shopid)
        {
            String sql = "UPDATE shops_product SET shops_product.Quantity =" + newamount + " WHERE shops_product.Products_Product_id =" + productid + " AND shops_product.Shop_Shop_id =" + shopid + ";";

            MySqlCommand command = new MySqlCommand(sql, connection);



            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

            }
            catch
            {
                MessageBox.Show("error while loading the people");
            }
            finally
            {
                connection.Close();
            }
        }


         public void UpdateBalance(int clientid, int amount)
        {
            String sql = "UPDATE client SET client.Balance = "+amount+" WHERE client.Client_id = "+clientid+";";
            MySqlCommand command = new MySqlCommand(sql, connection);



            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

            }
            catch
            {
                MessageBox.Show("error while loading the people");
            }
            finally
            {
                connection.Close();
            }
        }
        public bool MakePurchase(int clientid,int shopid,int price,List<string> sqlstatements)
         {
             bool successful = false;

             MySqlCommand command = connection.CreateCommand();
             try
             {
                 connection.Open();
                 command.CommandText = "SET autocommit=0;";
                 command.ExecuteNonQuery();
                 command.CommandText = "LOCK TABLES purchase WRITE,purchase as p2 READ,products_purchase WRITE,shops_product WRITE,products WRITE, client WRITE;";
                 command.ExecuteNonQuery();
                 command.CommandText = "INSERT INTO purchase(Purchase_id,Client_Client_id, Shop_Shop_id) VALUES((SELECT IFNULL(MAX(Purchase_id),0) FROM purchase p2)+1," + clientid + "," + shopid + ");";
                 command.ExecuteNonQuery();
                 foreach (var item in sqlstatements)
                 {
                     command.CommandText = item;
                     command.ExecuteNonQuery();
                 }
                 command.CommandText = "UPDATE client SET client.Balance = Balance - "+price+" WHERE client.Client_id = "+clientid+";";
                 command.ExecuteNonQuery();
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
        public string GetItemToPurchase(int productid,int quantity,int shopid)
        {
            string sqlstatement = " INSERT INTO products_purchase(Products_Product_id, Purchase_Purchase_id, Quantity)VALUES(" + productid + ",(SELECT IFNULL(MAX(Purchase_id),0) FROM purchase p2)," + quantity + ");" +
                                    " UPDATE products SET products.InStock = products.InStock - " + quantity + " WHERE products.Product_id =" + productid + ";" +
                                    " UPDATE shops_product SET shops_product.Quantity = shops_product.Quantity - " + quantity + " WHERE shops_product.Products_Product_id =" + productid + " AND shops_product.Shop_Shop_id ="+shopid+"; ";
            return sqlstatement;
        }

        public void CheckIfThereHaveBeenChanges(int currenttotal)
        {
            int latesttotal = this.GetTotalInStockForShop(1);
            
                if (currenttotal != latesttotal)
                {
                    if (EventDataChanged != null)
                    {
                        EventDataChanged();
                    }
                }        
            
        }

        public int GetTotalInStockForShop(int shopid)
        {
            String sql = "SELECT IFNULL(SUM(shops_product.Quantity),0) AS TOTALINSTOCK from shops_product WHERE shops_product.Shop_Shop_id = "+shopid+";";
            MySqlCommand command = new MySqlCommand(sql, connection);

            int totalinstock = -1;



            try
            {
               
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    totalinstock = Convert.ToInt16(reader["TOTALINSTOCK"]);

                }


            }
            
            
            finally
            {
                connection.Close();
            }
            return totalinstock;
        }


        
       
    }
}
