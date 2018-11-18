using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "BaseIngredient", menuName ="DarkLordGame/Create Data/Base Ingredient")]
    public class IngredientData : ScriptableObject
    {
        public int id;
        public string ingredientName = "Tap Water";
        public string description = "";
        public float baseCost = 100;
        public float costMultiplier = 1;

        //parameter --- ****multiplier, ****adder---
    }
}