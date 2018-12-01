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
        public MixingMethodType mixingMethod = MixingMethodType.None;

        public float effectiveScore = 0;
        public float tasteScore = 0;
        public float ability = 0;
        public float totalScore = 0;
    }
}