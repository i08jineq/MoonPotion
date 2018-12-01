using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "Unlock Ingredient", menuName = "DarkLordGame/UnlockData/Ingredient")]
    public class IngredientUnlockData : UnlockData
    {
        public IngredientData targetIngredientData;
        public bool isBaseIngredient = false;
        public override UnlockDataType GetUnlockType()
        {
            return UnlockDataType.Ingredient;
        }
    }
}