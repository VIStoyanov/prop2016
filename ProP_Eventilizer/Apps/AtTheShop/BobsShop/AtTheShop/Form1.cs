using Phidgets;
using Phidgets.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtTheShop
{
    public partial class Form1 : Form
    {
        Panel[] panels;
        bool[] imgcheck;
        PictureBox[] pictures;
         int selectedId = 0;
         private int totalPrice;

         string rfid;

        private RFID myRFIDReader;
        private DataHelper helper;
        private List<Item> items;
        private Label[] pricetags;
        private List<string> persondata;
        private int latesttotal;
         
        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Add("---------------------------------------------------------------------------------------------------------------");
            listBox1.Items.Add("Please, scan your bracelet on the device");
            listBox1.Items.Add("---------------------------------------------------------------------------------------------------------------");
            myRFIDReader = new RFID();
            myRFIDReader.Detach += new DetachEventHandler(SuccessfullyDetached);
            myRFIDReader.Tag += new TagEventHandler(ProcessThisTag);
            helper = new DataHelper();
            items = new List<Item>();
            pricetags = new Label[18];
            persondata = new List<string>();

            latesttotal = helper.GetTotalInStockForShop(2);
            helper.EventDataChanged += new DataHelper.DataChangedHandler(this.RefreshData);
            


            try
            {
                myRFIDReader.open();
                myRFIDReader.waitForAttachment(3000);

                myRFIDReader.Antenna = true;
                myRFIDReader.LED = true;
            }
            catch (PhidgetException)
            {
                MessageBox.Show("no RFID-reader opened. Please, attach RFID reader.");
                

            }
            panels = new Panel[18];
            imgcheck = new bool[18];
            pictures = new PictureBox[18];
            assignArrayValues();
           



            for (int i = 0; i < imgcheck.Length; i++)
            {
                imgcheck[i] = false;
            }
            for (int i = 0; i < pictures.Length; i++)
            {
                pictures[i].Click += pictureBoxClick;
            }
            
        }

        private void SuccessfullyDetached(object sender, DetachEventArgs e)
        {
            MessageBox.Show("RFIDReader detached!, serial nr: " + e.Device.SerialNumber.ToString());
        }
        private void ProcessThisTag(object sender, TagEventArgs e)
        {
            rfid = e.Tag;
           persondata = helper.GetInfoAboutPerson(rfid);
           if (persondata.Count > 0)
           {
               items = helper.GetAllProducts(items,2);
               this.RefreshListbox();
               SetPriceTags();
               timerCheck.Start();
              
               button1.Enabled = true;
               button2.Enabled = true;
               button3.Enabled = true;
              
           }
           else
           {
               MessageBox.Show("No matching person was found for that RFID. \nPlease contact an employee if your chip is defective.");
           }
       
        }

        private void RefreshListbox()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Hello " + persondata[1] + " " + persondata[2] + ". Your id is: " + persondata[0] + " and your balance is: " + persondata[3]);
            listBox1.Items.Add("*************************************************************************************");
            listBox1.Items.Add("---------------------------------------Your order------------------------------------------");
           

            foreach (var item in items)
            {
                if (item.Quantity > 0)
                {
                    listBox1.Items.Add(item.ToString());

                    totalPrice += item.Price * item.Quantity;
                }

            }
            listBox1.Items.Add("Total price of purchase: " + totalPrice);
           
        }

        private void SetPriceTags()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].InStock == 0)
                { 
                    pricetags[i].Text = "OUT OF STOCK"; 
                }
                else
                {
                    pricetags[i].Text = "Price: " + String.Format("{0:C}", items[i].Price);
                }
                
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void pictureBoxClick(object sender, EventArgs e)
        {

            for (int i = 0; i < pictures.Length; i++)
            {
                if (sender == pictures[i])
                {
                    panels[i].BackColor = Color.YellowGreen;
                    selectedId = i;
                    
                }
                else
                {
                    panels[i].BackColor = Color.Transparent;
                }
            }
           

        }

        private void assignArrayValues()
        {
            pictures[0] = pictureBoxFries;
            pictures[1] = pictureBoxPizza;
            pictures[2] = pictureBoxBurger;
            pictures[3] = pictureBoxIceCream;
            pictures[4] = pictureBoxCake;
            pictures[5] = pictureBoxDonut;
            pictures[6] = pictureBoxHotDog;
            pictures[7] = pictureBoxSandwich;
            pictures[8] = pictureBoxPopcorn;
            pictures[9] = pictureBoxCola;
            pictures[10] = pictureBoxFanta;
            pictures[11] = pictureBoxPepsi;
            pictures[12] = pictureBoxHeineken;
            pictures[13] = pictureBoxBacardi;
            pictures[14] = pictureBoxJegermeister;
            pictures[15] = pictureBoxSprite;
            pictures[16] = pictureBoxPerrier;
            pictures[17] = pictureBoxAmstel;



            panels[0] = panel1;
            panels[1] = panel2;
            panels[2] = panel3;
            panels[3] = panel4;
            panels[4] = panel5;
            panels[5] = panel6;
            panels[6] = panel7;
            panels[7] = panel8;
            panels[8] = panel9;
            panels[9] = panel10;
            panels[10] = panel11;
            panels[11] = panel12;
            panels[12] = panel13;
            panels[13] = panel14;
            panels[14] = panel15;
            panels[15] = panel16;
            panels[16] = panel17;
            panels[17] = panel18;

            pricetags[0] = labelPrice1;
            pricetags[1] = labelPrice2;
            pricetags[2] = labelPrice3;
            pricetags[3] = labelPrice4;
            pricetags[4] = labelPrice5;
            pricetags[5] = labelPrice6;
            pricetags[6] = labelPrice7;
            pricetags[7] = labelPrice8;
            pricetags[8] = labelPrice9;
            pricetags[9] = labelPrice10;
            pricetags[10] = labelPrice11;
            pricetags[11] = labelPrice12;
            pricetags[12] = labelPrice13;
            pricetags[13] = labelPrice14;
            pricetags[14] = labelPrice15;
            pricetags[15] = labelPrice16;
            pricetags[16] = labelPrice17;
            pricetags[17] = labelPrice18;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            items[selectedId].Quantity++;
            RefreshListbox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           items[selectedId].Quantity--;
           RefreshListbox();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
           
            if (CheckIfThereIsSomethingToAdd(items,items.Count-1))
            {
                DialogResult dialogResult = MessageBox.Show("Confirm purchase?", "Confirmation", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    if (Convert.ToDecimal(persondata[3]) >= totalPrice)
                    {
                        List<String> sqlstatements = new List<String>();
                      
                        string receipt = "";
                        receipt += "--------------------------------------Receipt----------------------------------------\nShop:Bob's Shop\n\nPurchased items:\n";
                        foreach (var item in items)
                        {
                            if (item.Quantity > 0)
                            {
                                


                                sqlstatements.Add(helper.GetItemToPurchase(item.ProductId, item.Quantity, 2));

                                receipt += "\n" + item.ToString() + "  price:" + item.Quantity * item.Price;
                                item.Quantity = 0;

                            }
                        }

                        receipt += "\n_____________________________________________________________________________________\nTotal Price: " + totalPrice + "\nDate: " + DateTime.Now;


                   

                        bool completed = helper.MakePurchase(Convert.ToInt32(persondata[0]), 2, totalPrice, sqlstatements);
                        if (completed == true)
                        { MessageBox.Show(receipt); RefreshListbox(); }
                        else
                        {
                            MessageBox.Show("Error processing the statement.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry! Insufficient funds in your balance.");
                    }

                }
                
               
            }
            else
            {
                MessageBox.Show("There are no items to purchase");
            }

        }

        private bool CheckIfThereIsSomethingToAdd(List<Item> list, int count)
        {
            if (list[count].Quantity > 0)
            {
                return true;
            }
            else
            {
                if (count == 0)
                {
                    return false;
                }
                else return CheckIfThereIsSomethingToAdd(list, count - 1);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myRFIDReader.Attached == true)
            {
                myRFIDReader.LED = false;
                myRFIDReader.Antenna = false;
                myRFIDReader.close();
            }
        }

        private void timerCheck_Tick(object sender, EventArgs e)
        {
            helper.CheckIfThereHaveBeenChanges(latesttotal);
        }

        private void RefreshData()
        {
            latesttotal = helper.GetTotalInStockForShop(2);
           
            items = helper.GetAllProducts(items,2);
            persondata = helper.GetInfoAboutPerson(rfid);
            totalPrice = 0;
            this.RefreshListbox();
        }

    }
}