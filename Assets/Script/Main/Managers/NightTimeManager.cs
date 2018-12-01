﻿using System.Collections;
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
        public ResultScreen resultScreen;
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
            yield return null;
            SetupResultScreen();
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

        private void SetupResultScreen()
        {
            resultScreen.Setup();
            resultScreen.onClosed.AddListener(OnCloseResultScreen);
        }
        #region event

        public void Start()
        {
            craftNewItemScreen.gameObject.SetActive(false);
            craftNewItemScreen.ResetUI();

            craftItemFromRecipeScreen.gameObject.SetActive(false);
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
            nightTimeUITopScreen.gameObject.SetActive(true);
            nightTimeUITopScreen.openShop.interactable = hasAnyItem;
            nightTimeUITopScreen.warningText.SetActive(!hasAnyItem);
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
                    //calculate effeciency
                    InventoryItemData inventoryItem = CalculateCraftItemScore();

                    Singleton.instance.currentSelectedSaveData.inventoryItemDatas.Add(inventoryItem);
                    Singleton.instance.SaveCurrentSlotData();

                    resultScreen.gameObject.SetActive(true);
                    resultScreen.StartCoroutine(resultScreen.ShowResultEnumerator(inventoryItem));
                    break;
            }
        }

        private InventoryItemData CalculateCraftItemScore()
        {
            InventoryItemData inventoryItem = craftNewItemScreen.GetCraftingItemData();
            float effectiveScore = 0;
            float tasteScore = 0;
            float universualScore = 0;
            float totalScore = (tasteScore + effectiveScore + universualScore) / 3;

            IngredientData currentBaseIngredientData = craftNewItemScreen.currentBaseIngredientData;
            List<IngredientData> currentIngredients = craftNewItemScreen.currentIngredients;
            effectiveScore += currentBaseIngredientData.baseEffectiveScore;
            tasteScore += currentBaseIngredientData.baseTasteScore;
            universualScore += currentBaseIngredientData.baseUniveresualScore;

            int count = currentIngredients.Count;
            for (int i = 0; i < count; i++)
            {
                effectiveScore += currentIngredients[i].baseEffectiveScore;
                tasteScore += currentIngredients[i].baseTasteScore;
                universualScore += currentIngredients[i].baseUniveresualScore;
            }

            effectiveScore = Mathf.Clamp(effectiveScore, 0, 10);
            tasteScore = Mathf.Clamp(tasteScore, 0, 10);
            universualScore = Mathf.Clamp(universualScore, 0, 10);

            inventoryItem.effectiveScore = Mathf.CeilToInt(effectiveScore);
            inventoryItem.tasteScore = Mathf.CeilToInt(tasteScore);
            inventoryItem.universualScore = Mathf.CeilToInt(universualScore);
            inventoryItem.totalScore = Mathf.CeilToInt(totalScore);
            inventoryItem.amount = 1;
            return inventoryItem;
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

        private void OnCloseResultScreen()
        {
            OpenTopScreen();
        }

        #endregion
    }
}