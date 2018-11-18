using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class InventoryItemData
    {
        public int recipeID = 0;
        public int amount = 0;
        public int baseIngredientID = 0;
        public List<int> ingredientIDs = new List<int>();
        public int mixingMethod = 0;
    }
}