using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DarkLordGame
{
    public class CraftNewItemScreen : MonoBehaviour
    {
        public Button craftButton;
        public Button cancelButton;

        public GameObject topScreenRoot;
        public Button selectBaseButton;
        public Button selectIngredientButton;
        public Button selectMixingMethodButton;

        public TextMeshProUGUI selectBaseIngredientButtonText;
        public TextMeshProUGUI selectIngredientButtonText;
        public TextMeshProUGUI selectMixingMethodButtonText;
        public TextMeshProUGUI craftingPrice;

        public SelectBaseIngredientScreen selectBaseIngredientScreen;
        public SelectIngredientScreen selectIngredientScreen;
        public SelectMixingMethodScreen selectMixingMethodScreen;

        public Communicator<UIEvent> uiEvent = new Communicator<UIEvent>();

        private InventoryItemData craftingItemData = new InventoryItemData();
        private int baseIngredientPrice = 0;
        private int ingredientsSumPrice = 0;

        public enum UIEvent
        {
            Cancel,
            Craft
        }

        public void Setup()
        {
            cancelButton.onClick.AddListener(OnClickedCancel);
            craftButton.onClick.AddListener(OnClickedCraft);
            selectBaseButton.onClick.AddListener(OnclickedSelectBase);
            selectIngredientButton.onClick.AddListener(OnClickedSelectIngredient);
            selectMixingMethodButton.onClick.AddListener(OnClickedMixingMethod);

            selectBaseIngredientScreen.Setup();
            selectBaseIngredientScreen.onClosed.AddListener(OnClosedSelectBaseIngredientScreen);
            selectBaseIngredientScreen.onSelectedBaseIngredientChanged.AddListener(UpdatePrice);
            selectBaseIngredientScreen.onSelectedBaseIngredientChanged.AddListener(UpdateBaseIngredient);

            selectIngredientScreen.Setup();
            selectIngredientScreen.onFinished.AddListener(OnClosedSelectIngredientScreen);
            selectIngredientScreen.onSelectingIngredientChanged.AddListener(UpdatePrice);
            selectIngredientScreen.onSelectingIngredientChanged.AddListener(UpdateIngredient);
        }

        //call everytime First try crafting
        public void ResetUI()
        {
            selectBaseIngredientButtonText.SetText("Pick Base Ingredient");
            selectIngredientButtonText.SetText("Pick Ingredient");
            selectMixingMethodButtonText.SetText("Pick Mixing Method");

            selectBaseIngredientButtonText.ForceMeshUpdate();
            selectIngredientButtonText.ForceMeshUpdate();
            selectMixingMethodButtonText.ForceMeshUpdate();

            selectBaseIngredientScreen.ResetUI();
            selectIngredientScreen.ResetUI();
            selectMixingMethodScreen.ResetUI();


            craftingPrice.SetText("0");
            craftingPrice.ForceMeshUpdate();
            baseIngredientPrice = 0;
            ingredientsSumPrice = 0;

            SetEnableCraftButton(false);

            topScreenRoot.SetActive(true);
            craftingItemData = new InventoryItemData();
        }

        public void Open()
        {
            selectBaseIngredientScreen.gameObject.SetActive(false);
            selectIngredientScreen.gameObject.SetActive(false);
            selectMixingMethodScreen.gameObject.SetActive(false);
            topScreenRoot.SetActive(true);
            gameObject.SetActive(true);
        }

        #region ui event

        public void OnClickedCancel()
        {
            gameObject.SetActive(false);
            uiEvent.Invoke(UIEvent.Cancel);
        }

        public void OnClickedCraft()
        {
            gameObject.SetActive(false);
            uiEvent.Invoke(UIEvent.Craft);
        }

        public void OnclickedSelectBase()
        {
            selectBaseIngredientScreen.gameObject.SetActive(true);
            SetactiveTopScreenUI(false);
        }

        public void OnClickedSelectIngredient()
        {
            selectIngredientScreen.gameObject.SetActive(true);
            SetactiveTopScreenUI(false);
        }

        public void OnClickedMixingMethod()
        {
            selectMixingMethodScreen.gameObject.SetActive(true);
            SetactiveTopScreenUI(false);
        }

        private void OnClosedSelectBaseIngredientScreen()
        {
            SetactiveTopScreenUI(true);
            UpdateBaseIngredient();
            UpdatePrice();
        }

        private void OnClosedSelectIngredientScreen()
        {
            SetactiveTopScreenUI(true);
            UpdateIngredient();
            UpdatePrice();
        }

        private void UpdateBaseIngredient()
        {
            IngredientData baseIngredient = selectBaseIngredientScreen.GetSelectedBaseIngredient();
            if (baseIngredient != null)
            {
                selectBaseIngredientButtonText.SetText(baseIngredient.ingredientName);
                selectBaseIngredientButtonText.ForceMeshUpdate();
                craftingItemData.baseIngredientID = baseIngredient.id;
                baseIngredientPrice = baseIngredient.price;
            }
        }

        private void UpdateIngredient()
        {
            IngredientData[] ingredients = selectIngredientScreen.GetIngredientDatas();
            int length = ingredients.Length;
            ingredientsSumPrice = 0;
            craftingItemData.ingredientIDs.Clear();
            for (int i = 0; i < length; i++)
            {
                craftingItemData.ingredientIDs.Add(ingredients[i].id);
                ingredientsSumPrice += ingredients[i].price;
            }
        }

        private void UpdatePrice()
        {
            craftingPrice.SetText(GetTotalPrice().ToString());
            craftingPrice.ForceMeshUpdate();
        }

        #endregion

        private void SetactiveTopScreenUI(bool activate)
        {
            craftButton.gameObject.SetActive(activate);
            cancelButton.gameObject.SetActive(activate);
            topScreenRoot.SetActive(activate);
        }

        public void SetEnableCraftButton(bool interactable)
        {
            craftButton.interactable = interactable;
        }

        public InventoryItemData GetCraftingItemData()
        {
            return craftingItemData;
        }

        public int GetTotalPrice()
        {
            return baseIngredientPrice + ingredientsSumPrice;
        }
    }
}