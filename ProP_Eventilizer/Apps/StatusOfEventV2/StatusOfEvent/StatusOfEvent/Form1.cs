using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatusOfEvent
{
    public partial class Form1 : Form
    {
        DataHelper helper = new DataHelper();
        List<string> AllPeople;
        List<string> AllArticles;
        List<string> AllShops;
        List<string> PresentLeftToArrive;
        public Form1()
        {
            InitializeComponent();
            AllPeople = helper.GetAllNames();
             AllArticles = helper.GetAllArticles();
             AllShops = helper.GetAllShops();
             this.RefreshData();
             timer1.Start();
            

        }

        private void RefreshData()
        {
            listBoxArticles.Items.Clear();
            listBoxVisitors.Items.Clear();
            listBoxShops.Items.Clear();
            for (int i = 0; i < AllPeople.Count; i += 2)
            {
                listBoxVisitors.Items.Add(AllPeople[i] + " " + AllPeople[i + 1]);

            }
            for (int i = 0; i < AllArticles.Count; i += 2)
            {
                listBoxArticles.Items.Add(AllArticles[i] + "." + AllArticles[i + 1]);
            }
            for (int i = 0; i < AllShops.Count; i += 2)
            {
                listBoxShops.Items.Add(AllShops[i] + ":" + AllShops[i + 1]);
            }

            PresentLeftToArrive = helper.GetStatusOfPeople();
            labelNrOfPresentLeftArriving.Text = "PRESENT:" + PresentLeftToArrive[0] + "\nLEFT:" + PresentLeftToArrive[2] + "\nTO ARRIVE:" + PresentLeftToArrive[1];
        }

        private void listBoxVisitors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] name =  Convert.ToString(listBoxVisitors.SelectedItem).Split(' ');
            listBoxStatus.Items.Clear();
            List<string> result = helper.GetStatusOfPerson(name[0], name[1]);
            listBoxStatus.Items.Add("Status:" + result[0]);
            listBoxStatus.Items.Add("Money spent at shops: " + result[1]);
            listBoxStatus.Items.Add("Amount paid for ticket: " + result[2]);
            listBoxStatus.Items.Add("Total amount transfered to account: " + result[3]);
            listBoxStatus.Items.Add("Balance of account: " + result[4]);

        }

        private void buttonInfoAboutAllEventAccounts_Click(object sender, EventArgs e)
        {
            List<string> InfoAboutAllAccounts = helper.GetInfoAboutAllAccounts();
            listBoxStatus.Items.Clear();
            listBoxStatus.Items.Add("Total money spent:" + InfoAboutAllAccounts[0]);
            listBoxStatus.Items.Add("Balance of all event accounts:" + InfoAboutAllAccounts[1]);
            listBoxStatus.Items.Add("How many visitors that booked a camping spot:" + InfoAboutAllAccounts[2]);
            listBoxStatus.Items.Add("Money spent on camping spots in total:" + InfoAboutAllAccounts[3]);
        }

        private void listBoxArticles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] article = Convert.ToString(listBoxArticles.SelectedItem).Split('.');
            listBoxStatus.Items.Clear();
            int amount = helper.GetSoldForOneArticle(article[0]);
            listBoxStatus.Items.Add(amount+" articles sold for item "+article[1]+" with id:" + article[0]);
            
        }

        private void listBoxShops_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] shop = Convert.ToString(listBoxShops.SelectedItem).Split(':');
            listBoxStatus.Items.Clear();
            int income = helper.GetAmountOfMoneySoldForAShop(shop[0]);
            
            listBoxStatus.Items.Add("Income for shop "+shop[1]+" with id:" + shop[0] + " is: " + income);
            
        }

        private void buttonStatusOfCampingSpot_Click(object sender, EventArgs e)
        {
            List<string> CampingSpots = helper.GetInfoAboutFreeCampingSpots();
            listBoxStatus.Items.Clear();
            listBoxStatus.Items.Add("Occupation\t" + "Tent Id");
            listBoxStatus.Items.Add("---------------------------------------");
            for (int i = 0; i < CampingSpots.Count; i += 2)
            {
                if(CampingSpots[i]!="FREE")
                listBoxStatus.Items.Add("TAKEN \t\t" + CampingSpots[i + 1]);
                else listBoxStatus.Items.Add(CampingSpots[i] + "\t\t" + CampingSpots[i + 1]);
               
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string name = textBoxVisitors.Text;

            for(int i =0;i<listBoxVisitors.Items.Count;i++)
            {
                if (((string)listBoxVisitors.Items[i]).ToUpper().Contains(name.ToUpper()))
                {
                    listBoxVisitors.SelectedItem = listBoxVisitors.Items[i];
                    break;
                }
            }
        }

        private void buttonSearchArticles_Click(object sender, EventArgs e)
        {
            string article = textBoxArticles.Text;

            for (int i = 0; i < listBoxArticles.Items.Count; i++)
            {
                if (((string)listBoxArticles.Items[i]).ToUpper().Contains(article.ToUpper()))
                {
                    listBoxArticles.SelectedItem = listBoxArticles.Items[i];
                    break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PresentLeftToArrive = helper.GetStatusOfPeople();
            labelNrOfPresentLeftArriving.Text = "PRESENT:" + PresentLeftToArrive[0] + "\nLEFT:" + PresentLeftToArrive[2] + "\nTO ARRIVE:" + PresentLeftToArrive[1];
        }

       
    }
}
