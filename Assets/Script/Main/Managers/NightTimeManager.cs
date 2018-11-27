using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class NightTimeManager
    {
        public NightTimeUITopScreen nightTimeUITopScreen;
        public CraftNewItemScreen craftNewItemScreen;
        public CraftItemFromRecipeScreen craftItemFromRecipeScreen;

        public Communicator onFinish = new Communicator();
        // craft data
        // on craft
        public IEnumerator SetupEnumerator()
        {
            SetupTopScreen();
            yield return null;
            SetupCraftNewItemTopScreen();
            yield return null;
            SetupCraftFromRecipeScreen();
        }

        private void SetupTopScreen()
        {
            nightTimeUITopScreen.Setup();
            nightTimeUITopScreen.uiEvent.AddListener(TopScreenEvent);
            nightTimeUITopScreen.gameObject.SetActive(false);
        }

        private void SetupCraftNewItemTopScreen()
        {
            craftNewItemScreen.Setup();
            craftNewItemScreen.gameObject.SetActive(false);
            craftNewItemScreen.uiEvent.AddListener(CraftNewItemScreenEvent);
        }

        private void SetupCraftFromRecipeScreen()
        {
            craftItemFromRecipeScreen.Setup();
            craftItemFromRecipeScreen.uiEvent.AddListener(CraftRecipeIem);
            craftItemFromRecipeScreen.gameObject.SetActive(false);
        }

        #region event

        public void Start()
        {
            craftNewItemScreen.gameObject.SetActive(false);
            craftNewItemScreen.ResetUI();

            craftItemFromRecipeScreen.gameObject.SetActive(false);
            //craftItemFromRecipeScreen.r

            OpenTopScreen();
        }

        private void OpenTopScreen()
        {
            List<InventoryItemData> inventories = Singleton.instance.currentSelectedSaveData.inventoryItemDatas;
            int numbers = inventories.Count;
            bool hasAnyItem = false;
            for (int i = 0; i < numbers; i++)
            {
                if(inventories[i].amount > 0)
                {
                    hasAnyItem = true;
                    break;
                }
            }
            nightTimeUITopScreen.openShop.interactable = hasAnyItem;
            nightTimeUITopScreen.warningText.SetActive(!hasAnyItem);
            nightTimeUITopScreen.gameObject.SetActive(true);

            nightTimeUITopScreen.craftFromRecipe.interactable = (numbers > 0);
        }

        private void TopScreenEvent(NightTimeUITopScreen.UIEvent eventType)
        {
            nightTimeUITopScreen.gameObject.SetActive(false);
            switch(eventType)
            {
                case NightTimeUITopScreen.UIEvent.Finish:
                    onFinish.Invoke();
                    break;
                case NightTimeUITopScreen.UIEvent.CraftNewItem:
                    craftNewItemScreen.Open();
                    break;
                case NightTimeUITopScreen.UIEvent.CraftFromRecipe:
                    craftItemFromRecipeScreen.gameObject.SetActive(true);
                    break;
            }
        }

        private void CraftNewItemScreenEvent(CraftNewItemScreen.UIEvent eventType)
        {
            craftNewItemScreen.gameObject.SetActive(false);
            switch (eventType)
            {
                case CraftNewItemScreen.UIEvent.Cancel:
                    nightTimeUITopScreen.gameObject.SetActive(true);
                    break;
                case CraftNewItemScreen.UIEvent.Craft:
                    //do craft stuff x 10
                    break;
            }
        }

        private void CraftRecipeIem(CraftItemFromRecipeScreen.UIEvent uIEvent)
        {
            craftItemFromRecipeScreen.gameObject.SetActive(false);
            switch(uIEvent)
            {
                case CraftItemFromRecipeScreen.UIEvent.Cancel:
                    OpenTopScreen();
                    break;
                case CraftItemFromRecipeScreen.UIEvent.DeleteRecipe:
                    //do delete stuff
                    break;
                case CraftItemFromRecipeScreen.UIEvent.Craft:
                    //do craft stuff
                    break;
            }
        }

        #endregion
    }
}