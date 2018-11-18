using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "BaseIngredient", menuName ="DarkLordGame/Create Data/Base Ingredient")]
    public class BaseIngredientData : ScriptableObject
    {
        public int id;
        public string ingredientName = "Tap Water";
        public int priceForCrafting = 100;
    }
}