using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Phidgets;
using Phidgets.Events;

namespace ReturnLoanMaterials
{
    public partial class Form1 : Form
    {
        private RFID myRFIDReader;
        private DataHelper helper;
        private List<Rent> rents;
        string rfidnumber;
        Rent selectedrent;
        int totalprice = 0;

        int selectedid;
        List<string> persondata;
        public Form1()
        {

            InitializeComponent();
            listBox1.Items.Add("-----------------------------------------------------------------------------------------------------");
            listBox1.Items.Add("Please, scan your bracelet on the device.");
            listBox1.Items.Add("-----------------------------------------------------------------------------------------------------");
            myRFIDReader = new RFID();
            myRFIDReader.Attach += new AttachEventHandler(SuccessfullyAttached);
            myRFIDReader.Detach += new DetachEventHandler(SuccessfullyDetached);
            myRFIDReader.Tag += new TagEventHandler(ProcessThisTag);
            helper = new DataHelper();
            rents = new List<Rent>();

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

        private void SuccessfullyDetached(object sender, DetachEventArgs e)
        {
            MessageBox.Show("RFIDReader detached!, serial nr: " + e.Device.SerialNumber.ToString());
        }
        private void ProcessThisTag(object sender, TagEventArgs e)
        {
            rfidnumber = e.Tag;
            persondata = helper.GetInfoAboutPerson(e.Tag);
            

            rents = helper.GetInfoAboutLoanMaterials(Convert.ToInt32(persondata[0]));

            this.RefreshListbox();
            
        }

        private void RefreshListbox()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox1.Items.Add("Hello " + persondata[1] + " " + persondata[2] + ".");
            listBox1.Items.Add("Your id is: " + persondata[0] + " and your balance is: " + persondata[3]);
            listBox1.Items.Add("Select a rent to return.");
            listBox1.Items.Add("****************************************************************************************");
            if (rents.Count == 0)
            {
                listBox1.Items.Add("There are no items to return");
            }
            else
            {
                foreach (var rent in rents)
                {
                    listBox1.Items.Add(rent);

                }
            }

        }


        private void buttonReturn_Click(object sender, EventArgs e)
        {
           

            if (selectedrent != null)
            {
                if (Convert.ToDecimal(persondata[3]) >= totalprice)
                {
                    bool success= helper.ReturnRent(selectedrent, Convert.ToInt32(persondata[0]), totalprice);
                    if (success == true)
                    {
                        persondata = helper.GetInfoAboutPerson(rfidnumber);
                        rents = helper.GetInfoAboutLoanMaterials(Convert.ToInt32(persondata[0]));
                        this.RefreshListbox();
                        
                       
                        listBox1.SelectedIndex = 0;
                        textBoxtimeofreturn.Text = "";
                        textBoxtimerented.Text = "";
                        textBoxtotalprice.Text = "";
                        textBoxtotaltime.Text = "";
                        MessageBox.Show("Returning rent with id " + selectedrent.RentId + " was successful.");
                        selectedrent = null;
                    }
                    
                        
                    
                }
                else
                {
                    MessageBox.Show("Your balance does not have the sufficient funds to process this request. \nPlease insert the required amount into your balance before returning the materials.");
                }
            }
            else
            {
                MessageBox.Show("Please select a rent to return from the list.");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            buttonReturn.Text = "Return";
            buttonReturn.Enabled = false;
            totalprice = 0;
            try
            {
                selectedrent = (Rent)listBox1.SelectedItem;
                
                int hours = (DateTime.Now - selectedrent.Timeofrent).Hours;
                if(hours==0)
                {
                    hours++;
                }
                int days = (DateTime.Now - selectedrent.Timeofrent).Days;
                int totaltime = (days * 24) + hours;
                foreach (var item in selectedrent.rentedItems)
                {
                    listBox2.Items.Add(item);
                    totalprice += item.Price * item.Quantity * totaltime;
                    
                }
                textBoxtimerented.Text = selectedrent.Timeofrent.ToString();
                textBoxtimeofreturn.Text = DateTime.Now.ToString();
               
                textBoxtotaltime.Text = Convert.ToString(totaltime);
                textBoxtotalprice.Text = Convert.ToString(totalprice);
                buttonReturn.Text = "Return items for rent with id: " + selectedrent.RentId;
                buttonReturn.Enabled = true;
            }
            catch
            {

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

    }

}

