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

namespace LoanMaterials
{
    public partial class Form1 : Form
    {
        private RFID myRFIDReader;
        private DataHelper helper;
        private List<Item> items;
        PictureBox[] pictureboxes;
        Panel[] panels;

        int latestquantity;

        int selectedid;
        List<string> persondata;
        
        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Add("----------------------------------------------------------------------------------------------------------------------------------------");
            listBox1.Items.Add("Please, scan your bracelet on the device");
            listBox1.Items.Add("----------------------------------------------------------------------------------------------------------------------------------------");
            myRFIDReader = new RFID();
            myRFIDReader.Attach += new AttachEventHandler(SuccessfullyAttached);
            myRFIDReader.Detach += new DetachEventHandler(SuccessfullyDetached);
            myRFIDReader.Tag += new TagEventHandler(ProcessThisTag);
            helper = new DataHelper();
            items = new List<Item>();

            helper.EventDataChanged += ReloadData;

            latestquantity = helper.CheckQuantity();

            pictureboxes = new PictureBox[4];
            pictureboxes[0] = pictureBoxCamera;
            pictureboxes[1] = pictureBoxTripod;
            pictureboxes[2] = pictureBoxPcCharger;
            pictureboxes[3] = pictureBoxUniversalCharger;

            panels = new Panel[4];
            panels[0] = panel1;
            panels[1] = panel2;
            panels[2] = panel3;
            panels[3] = panel4;

            for (int i = 0; i < pictureboxes.Length;i++ )
            {
                pictureboxes[i].Click += this.pictureBox_Click;
            }


                try
                {
                    myRFIDReader.open();
                    myRFIDReader.waitForAttachment(3000);

                    myRFIDReader.Antenna = true;
                    myRFIDReader.LED = true;
                }
                catch (PhidgetException)
                {
                    MessageBox.Show("no RFID-reader opened.");

                }
        }
        private void SuccessfullyAttached(object sender, AttachEventArgs e)
        {
            
        }

        private void ReloadData()
        {
            latestquantity = helper.CheckQuantity();
            items = helper.GetInfoAboutLoanMaterials(items);
           
            this.RefreshListbox();
        }

        private void SuccessfullyDetached(object sender, DetachEventArgs e)
        {
            MessageBox.Show("RFIDReader detached!, serial nr: " + e.Device.SerialNumber.ToString());
        }
        private void ProcessThisTag(object sender, TagEventArgs e)
        {
           persondata = helper.GetInfoAboutPerson(e.Tag);

             items = helper.GetInfoAboutLoanMaterials(items);
             if (persondata != null)
             {
                 this.RefreshListbox();
                 button1.Enabled = true;
                 button2.Enabled = true;
                 button3.Enabled = true;
                 timer1.Start();
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

     
        private void pictureBox_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pictureboxes.Length; i++)
            {
                if (sender == pictureboxes[i])
                {
                    panels[i].BackColor = Color.YellowGreen;
                    selectedid = i + 1;

                }
                else
                {
                    panels[i].BackColor = Color.Transparent;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.IncreaseQuantityForItem(selectedid);
          
        }
        private void IncreaseQuantityForItem(int id)
        {
            foreach (var item in items)
            {
                if (id == item.EquipmentId)
                {
                    item.Quantity++;


                }
            
            }
            this.RefreshListbox();
        }
        private void DecreeaseQuantityForItem(int id)
        {
            foreach (var item in items)
            {
                if (id == item.EquipmentId)
                {
                    item.Quantity--;
                    
                   
                }
                
            
            }
            this.RefreshListbox();
           
        }
        private bool CheckIfThereIsSomethingToAdd(List<Item> list, int count)
        {
            if (list[count].Quantity>0)
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

        private void RefreshListbox()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Hello " + persondata[1] + " " + persondata[2] + ". Your id is: " + persondata[0] + " and your balance is: " + persondata[3]);
            listBox1.Items.Add("**********************************************************************************************************************************");
           foreach (var item in items)
            {
                 listBox1.Items.Add(item);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DecreeaseQuantityForItem(selectedid);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!CheckIfThereIsSomethingToAdd(items,items.Count-1))
            {
                MessageBox.Show("You haven't selected any items.");
            }
            else
            {
               
                bool success = helper.MakeRent(Convert.ToInt32(persondata[0]), items);
                if (success == true)
                {
                    MessageBox.Show("Rent successful.");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = listBox1.SelectedItem;
            if(item is Item)
            {
                selectedid = ((Item)item).EquipmentId;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            helper.CheckIfThereHaveBeenChanges(latestquantity);
        }
    }
}
