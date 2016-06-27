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

namespace AtTheEntranceV2
{
    public partial class FormLogged : Form
    {
        public int barcode;
        private RFID myRFIDReader;
        private DataHelper helper = new DataHelper();
        private List<string> clientdata;
        public FormLogged(int bcode)
        {
            InitializeComponent();
            barcode = bcode;
            clientdata = helper.GetClient(barcode);
            myRFIDReader = new RFID();
            
            myRFIDReader.Tag += new TagEventHandler(ProcessThisTag);
            if(clientdata.Count != 0)
            {
                listBoxInfo.Items.Add("Client id: " + clientdata[0]);
                listBoxInfo.Items.Add("First Name: " + clientdata[1]);
                listBoxInfo.Items.Add("Last Name: " + clientdata[2]);
                listBoxInfo.Items.Add("E-mail: " + clientdata[3]);
            }
            else
            {
                listBoxInfo.Items.Add("No person was found with that barcode!");
                buttonAssignRFID.Enabled = false;
            }
            
        }

        
        private void buttonGoBack_Click(object sender, EventArgs e)
        {
            FormLogged.ActiveForm.Close();
        }

        private void buttonAssignRFID_Click(object sender, EventArgs e)
        {
            try
            {
                myRFIDReader.open();
                myRFIDReader.waitForAttachment(3000);

                myRFIDReader.Antenna = true;
                myRFIDReader.LED = true;
                MessageBox.Show("Scan the RFID");
            }
            catch (PhidgetException) { MessageBox.Show("no RFID-reader opened."); }
        }
        private void ProcessThisTag(object sender, TagEventArgs e)
        {
            helper.UpdateRFIDAndChangeStatus(Convert.ToInt32(clientdata[0]), e.Tag);
        }

        private void FormLogged_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(myRFIDReader.Attached==true)
            {
                myRFIDReader.LED = false;
                myRFIDReader.Antenna = false;
                myRFIDReader.close();
            }
        }
       
    }
}
