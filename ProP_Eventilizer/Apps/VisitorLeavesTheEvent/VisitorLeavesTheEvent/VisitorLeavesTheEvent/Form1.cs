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

namespace VisitorLeavesTheEvent
{
    public partial class Form1 : Form
    {
        private RFID myRFIDReader;
        private DataHelper helper;
        private List<string> persondata;
        bool rfidattached = true;


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
            persondata = new List<string>();

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
                rfidattached = false;
               
            }
        }

        private void SuccessfullyDetached(object sender, DetachEventArgs e)
        {
            MessageBox.Show("RFIDReader detached!, serial nr: " + e.Device.SerialNumber.ToString());
        }
        private void ProcessThisTag(object sender, TagEventArgs e)
        {
            persondata = helper.GetInfoAboutPerson(e.Tag);
            if (persondata.Count>0)
            {
                buttonCheckOut.Enabled = true;
                this.RefreshListbox();
            }
            else
            {
                MessageBox.Show("No matching person was found for that RFID. \nPlease contact an employee if your chip is defective.");
            }
        }

        private void RefreshListbox()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("*************************************************************************************");
            listBox1.Items.Add("Hello " + persondata[1] + " " + persondata[2] + ". Your id is: " + persondata[0] + " and your balance is: " + persondata[3]);
            listBox1.Items.Add("*************************************************************************************");

            
        }

        private void buttonCheckOut_Click(object sender, EventArgs e)
        {
           
            int result = helper.CheckIfPersonHasToReturn(Convert.ToInt16(persondata[0]));
            if (result!=0)
            {
                MessageBox.Show("You have: " + result + " item(s) to return. Please go to the Return Stand first and give back all the rented materials.");
            }
            else
            {
                bool success = helper.UnassignRFIDAndChangeStatus(Convert.ToInt32(persondata[0]));
                if (success)
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("---------------------------------------------------------------------------------------------------------------");
                    listBox1.Items.Add("Please, scan your bracelet on the device");
                    listBox1.Items.Add("---------------------------------------------------------------------------------------------------------------");
                    if (Convert.ToDecimal(persondata[3]) > 0)
                    {
                        MessageBox.Show("You have " + persondata[3] + " in your balance.\nPlease ask one of the employees to give you your money back. Thank you!");
                    }
                    MessageBox.Show("You have been successfuly checked out.");
                    persondata = null;
                    buttonCheckOut.Enabled = false;

                }
               
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

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (rfidattached == false)
            {
                Form.ActiveForm.Close();
            }
        }


           

    }
}

