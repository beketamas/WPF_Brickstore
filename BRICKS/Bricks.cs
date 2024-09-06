using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRICKS
{
    public class Bricks
    {
        string itemID;
        string itemName;
        string categoryName;
        string colorName;
        int qty;

        public Bricks(string sor)
        {
            string[] tomb = sor.Split(';');
            itemID = tomb[0];
            itemName = tomb[1];
            categoryName = tomb[2];
            colorName = tomb[3];
            qty = int.Parse(tomb[4]);
        }

        public string ItemID  => itemID;
        public string ItemName => itemName;
        public string CategoryName => categoryName;
        public string ColorName  => colorName; 
        public int Qty  => qty;
    }
}
