using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "BaseIngredient", menuName ="DarkLordGame/Create Data/Base Ingredient")]
    public class IngredientData : ScriptableObject
    {
        public int id;
        public string ingredientName = "Tap Water";
        public string description = "";
        public int price = 100;

        //parameter --- ****multiplier, ****adder---
    }
}