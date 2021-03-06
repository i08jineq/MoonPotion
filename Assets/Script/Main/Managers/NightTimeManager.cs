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
        public InventoryScreen inventoryScreen;
        public ResultScreen resultScreen;

        public MixingMethodData mixingMethodData;

        public Communicator onCraftedNewItem = new Communicator();
        public Communicator onFinish = new Communicator();
        public Communicator onGoldChanged = new Communicator();
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
            inventoryScreen.Setup();
            inventoryScreen.onClosed.AddListener(OnClosedInventoryScreen);
            inventoryScreen.gameObject.SetActive(false);
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

            inventoryScreen.gameObject.SetActive(false);
            OpenTopScreen();
        }

        private void OpenTopScreen()
        {
            List<InventoryItemData> inventories = Singleton.instance.saveData.inventoryItemDatas;
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
                    inventoryScreen.gameObject.SetActive(true);
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

                    Singleton.instance.saveData.inventoryItemDatas.Add(inventoryItem);
                    Singleton.instance.saveData.totalCraft++;
                    Singleton.instance.saveData.currentGold -= craftNewItemScreen.GetTotalPrice();
                    Singleton.instance.SaveData();

                    onGoldChanged.Invoke();

                    inventoryScreen.OnAddedNewInventory(inventoryItem);

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
            float abilityScore = 0;

            IngredientData currentBaseIngredientData = craftNewItemScreen.currentBaseIngredientData;
            List<IngredientData> currentIngredients = craftNewItemScreen.currentIngredients;
            effectiveScore += currentBaseIngredientData.baseEffectiveScore;
            tasteScore += currentBaseIngredientData.baseTasteScore;
            abilityScore += currentBaseIngredientData.baseAbilityScore;

            int count = currentIngredients.Count;
            for (int i = 0; i < count; i++)
            {
                effectiveScore += currentIngredients[i].baseEffectiveScore;
                tasteScore += currentIngredients[i].baseTasteScore;
                abilityScore += currentIngredients[i].baseAbilityScore;
            }

            MixingMethodData.MixingMethodMultiplier multiplier = mixingMethodData.GetMultiplier(inventoryItem.mixingMethod);
            effectiveScore *= multiplier.effectiveMultiplier;
            tasteScore *= multiplier.tasteMultiplier;
            abilityScore *= multiplier.abilityMultiplier;

            effectiveScore = Mathf.Clamp(effectiveScore, 1, 10);
            tasteScore = Mathf.Clamp(tasteScore, 1, 10);
            abilityScore = Mathf.Clamp(abilityScore, 1, 10);

            float totalScore = (tasteScore + effectiveScore + abilityScore) / 3;

            inventoryItem.effectiveScore = effectiveScore;
            inventoryItem.tasteScore = tasteScore;
            inventoryItem.ability = abilityScore;
            inventoryItem.totalScore = totalScore;
            inventoryItem.amount = 1;
            
            return inventoryItem;
        }

        private void OnClosedInventoryScreen()
        {
            OpenTopScreen();
        }

        private void OnCloseResultScreen()
        {
            OpenTopScreen();
            onCraftedNewItem.Invoke();
        }

        #endregion
    }
}