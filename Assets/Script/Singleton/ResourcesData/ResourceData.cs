using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class ResourceData
    {
        public IngredientData[] baseIngredientDatas;
        public IngredientData[] ingredientDatas;
        private int allBaseIngredientNumbers = 0;
        private int allIngredientNumbers = 0;
        private const string BaseIngredientPath = "ScriptableObjects/BaseIngredient/";
        private const string IngredientPath = "ScriptableObjects/Ingredient/";
        #region load
        public IEnumerator LoadResources()
        {
            LoadBaseIngredientDatas();
            yield return null;
        }

        private void LoadBaseIngredientDatas()
        {
            baseIngredientDatas = Resources.LoadAll<IngredientData>(BaseIngredientPath);
            allBaseIngredientNumbers = baseIngredientDatas.Length;
        }

        private void LoadIngredientDatas()
        {
            ingredientDatas = Resources.LoadAll<IngredientData>(IngredientPath);
            allIngredientNumbers = ingredientDatas.Length;
        }

        private IEnumerator RequestDelay(ResourceRequest request)
        {
            while (request.isDone == false)
            {
                yield return null;
            }
            yield return null;
        }

        #endregion
        #region Getter

        public IngredientData GetBaseIngredientData(int id)
        {
            for (int i = 0; i < allBaseIngredientNumbers; i++)
            {
                if (baseIngredientDatas[i].id == id)
                {
                    return baseIngredientDatas[i];
                }
            }
            return baseIngredientDatas[0];
        }

        public IngredientData GetIngredient(int id)
        {
            for (int i = 0; i < allIngredientNumbers; i++)
            {
                if (ingredientDatas[i].id == id)
                {
                    return ingredientDatas[i];
                }
            }
            return ingredientDatas[0];
        }
        #endregion
    }
}