using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaypalLogFileReader
{
    public partial class Form1 : Form
    {
        string filename;
       
        DateTime date;
        int numofdeposits;
        string bankaccountnum;
      


        DataHelper helper;
        FileStream fs = null;
        StreamReader sr = null;
        public Form1()
        {
            InitializeComponent();
            helper = new DataHelper();
        }
       

        private void buttonReadLogFile_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
              
                
                filename = openFileDialog1.FileName;
                
                try
                {
                  
                    fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs);
                    bankaccountnum = Convert.ToString(sr.ReadLine());
                    labelBankAccount.Text = bankaccountnum;


                    date = Convert.ToDateTime(Convert.ToString(sr.ReadLine()));
                  

                    numofdeposits = Convert.ToInt32(sr.ReadLine());

                    helper.MakeListOfTransactions(sr, numofdeposits, date,listBoxLog);
                     
                  
                   
                }
                catch (IOException)
                {
                    MessageBox.Show("something went wrong, IOException was thrown");
                }
                finally
                {
                    if (sr != null) sr.Close();
                    if (fs != null) fs.Close();
                }
            }
           
           
        }
    }
}
