﻿using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "BaseIngredient", menuName ="DarkLordGame/Create Data/Base Ingredient")]
    public class IngredientData : ScriptableObject
    {
        [IDGenerator("Ingredient", 100000)]public int id;
        public string ingredientName = "Tap Water";
        public string description = "";
        public int price = 100;

        //parameter --- ****multiplier, ****adder---
        public float baseEffectiveScore = 1;
        public float baseTasteScore = 1;
        public float baseAbilityScore = 1;
    }
}