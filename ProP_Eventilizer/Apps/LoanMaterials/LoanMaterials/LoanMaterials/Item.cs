using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanMaterials
{
    class Item
    {
        private int equipmentId, quantity, price, instock;
        private string name;

        public int EquipmentId
        {
            get { return equipmentId; }
            private set { equipmentId = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set 
            {
                if (value < 0)
                {
                    quantity = 0;
                }
                else if (value>InStock)
                {
                    quantity = InStock;
                }
                else quantity = value;
            
            }
        }
        public int Price
        {
            get { return price; }
            private set { price = value; }
        }
        public int InStock
        {
            get { return instock; }
            private set { instock = value; }
        }
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public Item (int id,string name,int price, int instock,int quantity)
        {
            EquipmentId = id;
            Name = name;
            Price = price;
            InStock = instock;
            Quantity = quantity;
            
        }

        public override string ToString()
        {
            return "Id: " + EquipmentId + "  Description: " + Name + "  Price per hour: " + Price + "  Quantity to purchase: " + Quantity + "  In Stock: " + InStock;
        }

    }
}
