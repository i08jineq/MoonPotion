using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class CraftingManager
    {
        protected CraftUI craftUI;

        public IEnumerator SetupEnumerator()
        {
            SetupCraftUI();

            yield break;
        }

        private void SetupCraftUI()
        {
            GameObject craftUIObject = GameObject.Instantiate<GameObject>(Singleton.instance.resourceData.craftUIPrefab, Singleton.instance.mainCanvasTransform);
            craftUI = craftUIObject.GetComponent<CraftUI>();
            craftUIObject.SetActive(false);
        }
    }
}