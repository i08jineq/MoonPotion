using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class InventoryItemData
    {
        public int id;
        public int amount;

        public InventoryItemData()
        {
            id = 0;
            amount = 1;
        }

        public InventoryItemData(int _id, int _amount)
        {
            id = _id;
            amount = _amount;
        }
    }
}