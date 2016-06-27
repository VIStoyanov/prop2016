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

namespace CampingEntrance
{
    public partial class Form1 : Form
    {
        private RFID myRFIDReader;
        private dataHelper helper;
        private List<PictureBox> tentpboxes;
      
       

        int selectedid;
        Person client;

        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Add("-----------------------------------------------------------------------------------------------------");
            listBox1.Items.Add("Please, scan your bracelet on the device");
            listBox1.Items.Add("-----------------------------------------------------------------------------------------------------");
            myRFIDReader = new RFID();
            myRFIDReader.Attach += new AttachEventHandler(SuccessfullyAttached);
            myRFIDReader.Detach += new DetachEventHandler(SuccessfullyDetached);
            myRFIDReader.Tag += new TagEventHandler(ProcessThisTag);
            helper = new dataHelper();
            tentpboxes = new List<PictureBox>();
            tentpboxes.Add(pictureBox1);
            tentpboxes.Add(pictureBox2);
            tentpboxes.Add(pictureBox3);
            tentpboxes.Add(pictureBox4);
            tentpboxes.Add(pictureBox5);
            tentpboxes.Add(pictureBox6);
            tentpboxes.Add(pictureBox7);
            tentpboxes.Add(pictureBox8);
            tentpboxes.Add(pictureBox9);
            tentpboxes.Add(pictureBox10);
            tentpboxes.Add(pictureBox11);
            tentpboxes.Add(pictureBox12);
            tentpboxes.Add(pictureBox13);
            tentpboxes.Add(pictureBox14);
            tentpboxes.Add(pictureBox15);


         
            


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
            client = helper.GetInfoAboutPerson(e.Tag);
            if (client != null)
            {
                foreach (var item in tentpboxes)
                {
                    item.BackColor = Color.DarkRed;
                }

                this.RefreshListbox();

                List<Group> groups = helper.GetTheHostAndGuest();
                bool found = false;
                foreach (var group in groups)
                {
                    if (group.Host.Id == client.Id)
                    {
                        listBox1.Items.Add(client.FirstName + " " + client.LastName + " you are the host of tent :" + group.TentSpotId);

                        if (group.Guests.Count > 0)
                        {
                            listBox1.Items.Add("Your guests:");
                            foreach (var guest in group.Guests)
                            {
                                listBox1.Items.Add(guest.FirstName + " " + guest.LastName + " id:" + guest.Id);
                            }
                        }
                        else
                        {
                            listBox1.Items.Add("No other guests are expected");
                        }
                        this.FindTent(group.TentSpotId);
                        found = true;
                        break;

                    }
                    else if (group.Host.Id == client.HostId)
                    {
                        listBox1.Items.Add(client.FirstName + ", your host is:" + group.Host.FirstName + " " + group.Host.LastName);
                        if (group.Guests.Count > 1)
                        {
                            listBox1.Items.Add("Other guests in the same tent:");
                            foreach (var guest in group.Guests)
                            {
                                if (guest.Id != client.Id)
                                {
                                    listBox1.Items.Add(guest.FirstName + " " + guest.LastName + " id:" + guest.Id);
                                }
                            }
                        }
                        else
                        {
                            listBox1.Items.Add("No other guests are expected");
                        }
                        this.FindTent(group.TentSpotId);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    MessageBox.Show("You were not found on the list of guests registered for a tent spot.\nIf you believe there is an error - contact an employee.");
                }

            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("-----------------------------------------------------------------------------------------------------");
                listBox1.Items.Add("Please, scan your bracelet on the device");
                listBox1.Items.Add("-----------------------------------------------------------------------------------------------------");
                foreach (var item in tentpboxes)
                {
                    item.BackColor = Color.DarkRed;
                }
                MessageBox.Show("No matching person was found for that RFID. \nPlease contact an employee if your chip is defective.");
            }




        }

      

        private void RefreshListbox()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Hello " + client.FirstName + " " + client.LastName + ". Your id is: " + client.Id);
            listBox1.Items.Add("*******************************************************************************");
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Group> groups = helper.GetTheHostAndGuest();
            bool found = false;
            foreach (var group in groups)
            {
                if (group.Host.Id == client.Id)
                {
                    listBox1.Items.Add(client.FirstName + " " + client.LastName + " you are the host of tent :" + group.TentSpotId);
                   
                    if (group.Guests.Count > 0)
                    {
                        listBox1.Items.Add("Your guests:");
                        foreach(var guest in group.Guests)
                        {
                            listBox1.Items.Add(guest.FirstName + " " + guest.LastName + " id:" + guest.Id);
                        }
                    }
                    else
                    {
                        listBox1.Items.Add("No other guests are expected");
                    }
                    this.FindTent(group.TentSpotId);
                    found = true;
                    break;

                }
                else if (group.Host.Id == client.HostId)
                {
                    listBox1.Items.Add(client.FirstName+", your host is:"+group.Host.FirstName + " " +group.Host.LastName);
                    if (group.Guests.Count > 1)
                    {
                        listBox1.Items.Add("Other guests in the same tent:");
                        foreach (var guest in group.Guests)
                        {
                            if (guest.Id != client.Id)
                            {
                                listBox1.Items.Add(guest.FirstName + " " + guest.LastName + " id:" + guest.Id);
                            }
                        }
                    }
                    else
                    {
                        listBox1.Items.Add("No other guests are expected");
                    }
                    this.FindTent(group.TentSpotId);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                MessageBox.Show("You were not found on the list of guests registered for a tent spot.\nIf you believe there is an error - contact an employee.");
            }
            
        }

        private void FindTent(int id)
        {
            for (int i = 0; i < tentpboxes.Count; i++)
            {
                if (i+1 == id)
                {
                    tentpboxes[i].BackColor = Color.LawnGreen;
                }
            }
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
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
