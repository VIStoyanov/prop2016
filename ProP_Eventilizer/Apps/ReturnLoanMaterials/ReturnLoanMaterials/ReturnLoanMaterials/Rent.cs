using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnLoanMaterials
{
    class Rent
    {
        public List<Item> rentedItems;
        public int RentId;
        public DateTime Timeofrent;
        public DateTime Timeofreturn;


        public Rent(int rentid,DateTime timeofrent)
        {
            rentedItems = new List<Item>();
            RentId = rentid;
            Timeofrent = timeofrent;
           
        }
        public void InsertItemintoRent(Item item)
        {
            rentedItems.Add(item);
        }
        public override string ToString()
        {
            return "Rent id:" + RentId + ", Time of rent: " + Timeofrent + ", items rented in total: " + rentedItems.Count; 
        }
    }
}
