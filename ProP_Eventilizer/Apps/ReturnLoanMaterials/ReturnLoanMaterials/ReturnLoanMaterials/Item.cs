using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnLoanMaterials
{
    class Item
    {
        private int equipmentId, quantity, price;
        private string name;

        public int EquipmentId
        {
            get { return equipmentId; }
            private set { equipmentId = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set{ quantity = value; }
        }
        public int Price
        {
            get { return price; }
            private set { price = value; }
        }
       
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public Item(int id, string name, int price, int quantity)
        {
            EquipmentId = id;
            Name = name;
            Price = price;
            Quantity = quantity;

        }

        public override string ToString()
        {
            return "Id:" + EquipmentId + "  Description:" + Name + "  Price:" + Price + "  Quantity:" + Quantity;
        }

    }
}
