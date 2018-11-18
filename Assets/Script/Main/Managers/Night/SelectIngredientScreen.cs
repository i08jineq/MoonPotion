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
            for (int i = 0; i < number; i++)
            {

            }
        }

        public void ResetUI()
        {
            selectingIngredientDatas.Clear();
        }
    }
}