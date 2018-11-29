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

        public int currentDay = 1;
        public int currentGold = 0;
        public List<int> unlockedCustomerID = new List<int>();
        public int totalSell = 0;
        public List<InventoryItemData> inventoryItemDatas = new List<InventoryItemData>();
        public List<int> unlcokedBaseIngredientID = new List<int>();
        public List<int> unlcokedIngredientID = new List<int>();
        public List<int> unlockedMixingMethodID = new List<int>();
        public List<int> completedEventID = new List<int>();

        //default save data
        public SaveData()
        {
            currentGold = 1000;
            unlockedMixingMethodID.Add(0);
            unlcokedBaseIngredientID.Add(0);
            unlcokedIngredientID.Add(0);
            unlockedMixingMethodID.Add(0);
        }
    }
}