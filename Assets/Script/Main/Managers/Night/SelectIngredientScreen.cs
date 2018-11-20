using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class SelectIngredientScreen : MonoBehaviour
    {
        public IngredientButton ingredientButtonPrefab;
        public Transform ingreDientButtonRoot;
        private List<IngredientData> selectingIngredientDatas = new List<IngredientData>();

        private const int selectAbleIngredientNumber = 3;

        public void Setup()
        {
            List<int> ingredientIDs = Singleton.instance.currentSelectedSaveData.unlcokedIngredientID;
            int number = ingredientIDs.Count;
            IngredientButton cahcedIngredientButton = GameObject.Instantiate<IngredientButton>(ingredientButtonPrefab, ingreDientButtonRoot);

            for (int i = 0; i < number; i++)
            {
                IngredientButton ingredientButton = GameObject.Instantiate<IngredientButton>(cahcedIngredientButton, ingreDientButtonRoot);
                IngredientData ingredientData = Singleton.instance.resourceData.GetIngredient(ingredientIDs[i]);
                ingredientButton.Setup(ingredientData);
                ingredientButton.onSelected.AddListener(OnSelected);
            }

            GameObject.Destroy(cahcedIngredientButton.gameObject);
        }

        private void OnSelected(bool selected, IngredientData ingredientData)
        {
            if(selected)
            {
                selectingIngredientDatas.Add(ingredientData);
            }else
            {
                selectingIngredientDatas.Remove(ingredientData);
            }
        }

        public void ResetUI()
        {
            selectingIngredientDatas.Clear();
        }
    }
}