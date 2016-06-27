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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost/site/entrance.php?token=z3rGru5h");
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            int barcode = Convert.ToInt32(this.textBoxBarcode.Text);
            FormLogged formLogged= new FormLogged(barcode);
         
            formLogged.Show();
            textBoxBarcode.Text = "";
            barcode = 0;
        }

        private void textBoxBarcode_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxBarcode.Text != "")
            {
                try
                {
                    Convert.ToInt32(this.textBoxBarcode.Text);
                    buttonLogin.Enabled = true;
                }
                catch
                {
                    buttonLogin.Enabled = false;
                    MessageBox.Show("Please provide correct input");

                }
            }
            else
            {
                buttonLogin.Enabled = false;
            }
        }
    }
}
