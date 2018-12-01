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
        public bool isBackroupted = false;
        public int currentDay = 0;
        public int currentGold = 0;
        public int totalCraft = 0;
        public int totalSell = 0;
        public List<InventoryItemData> inventoryItemDatas = new List<InventoryItemData>();
        public List<int> unlcokedBaseIngredientID = new List<int>();
        public List<int> unlcokedIngredientID = new List<int>();
        public List<MixingMethodType> unlockedMixingMethodID = new List<MixingMethodType>();
        public List<int> completedEventID = new List<int>();
        public Trending trending = new Trending();

        //default save data
        public SaveData()
        {
            currentGold = 2500;
            unlcokedBaseIngredientID.Add(0);
            unlcokedBaseIngredientID.Add(1);
            unlcokedIngredientID.Add(0);
            unlcokedIngredientID.Add(1);
            unlcokedIngredientID.Add(2);
            unlcokedIngredientID.Add(3);
            unlockedMixingMethodID.Add(MixingMethodType.Boil);
            unlockedMixingMethodID.Add(MixingMethodType.Freeze);
        }
    }
}