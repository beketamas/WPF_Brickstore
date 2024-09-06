using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRICKS
{
    internal class Bricks
    {
        string itemID;
        string itemName;
        string CategoryName;
        string ColorName;
        int Qty;

        public Bricks(string sor)
        {
            string[] tomb = sor.Split(';');
            itemID = tomb[0];
            itemName = tomb[1];
            CategoryName = tomb[2];
            ColorName = tomb[3];
            Qty = int.Parse(tomb[4]);
        }

        public string ItemID  => itemID;
        public string ItemName => itemName;
        public string CategoryName1 => CategoryName;
        public string ColorName1  => ColorName; 
        public int Qty1  => Qty;
    }
}
