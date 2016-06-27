using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtTheShop
{
    class Item
    {
        private int productId, quantity, price, instock, instocktotal;
        private string description;

        public int ProductId
        {
            get { return productId; }
            private set { productId = value; }
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
                else if (value > InStock)
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
        public int InStockTotal
        {
            get { return instocktotal; }
            private set { instocktotal = value; }
        }
        public string Description
        {
            get { return description; }
            private set { description = value; }
        }

        public Item(int id, string descr, int price, int instock,int instocktotal,int quantity)
        {
            ProductId = id;
            Description = descr;
            Price = price;
            InStock = instock;
            InStockTotal = instocktotal;
            Quantity = quantity;

        }

        public override string ToString()
        {
            return "Id:" + ProductId + "  Description:" + Description + "  Price per item:" + Price + "  Quantity:" + Quantity + "  In Stock:" + InStock;
        }

    }
}
