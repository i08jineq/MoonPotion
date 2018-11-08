using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkLordGame
{

    [System.Serializable]
    public class SaveData
    {
        public string playername = "-";
        public string shopName = "-";

        public int currentDay = 0;
        public int currentGold = 0;
        public List<int> unlockedCustomerID = new List<int>();
        public int totalSell = 0;
        public List<InventoryItemData> inventoryItemDatas = new List<InventoryItemData>();
        public List<int> unlockedMixingMethodID = new List<int>();
        public List<int> completedEventID = new List<int>();

        //default save data
        public SaveData()
        {
            currentGold = 1000;
            inventoryItemDatas.Add(new InventoryItemData(1, 20));
            unlockedMixingMethodID.Add(0);
        }
    }
}