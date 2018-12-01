using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class InventoryItemData
    {
        public string itemName = "";
        public int amount = 0;
        public int baseIngredientID = 0;
        public List<int> ingredientIDs = new List<int>();
        public MixingMethodType mixingMethod = MixingMethodType.Boil;

        public int effectiveScore = 0;
        public int tasteScore = 0;
        public int ability = 0;
        public int totalScore = 0;
    }
}