using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class ResourceData
    {
        public BaseIngredientData[] baseIngredientDatas;
        private int allBaseIngredientNumbers = 0;
        private const string BaseIngredientPath = "ScriptableObjects/BaseIngredient/";
        #region load
        public IEnumerator LoadResources()
        {
            LoadBaseIngredientDatas();
            yield return null;
        }

        private void LoadBaseIngredientDatas()
        {
            baseIngredientDatas = Resources.LoadAll<BaseIngredientData>(BaseIngredientPath);
            allBaseIngredientNumbers = baseIngredientDatas.Length;
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

        public BaseIngredientData GetBaseIngredientData(int id)
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
        #endregion
    }
}