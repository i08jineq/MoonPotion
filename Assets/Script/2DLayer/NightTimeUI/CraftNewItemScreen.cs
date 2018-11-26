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
            selectIngredientScreen.Setup();
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

            selectBaseIngredientScreen.onClosed.AddListener(OnClosedSelectBaseIngredientScreen);

            craftingPrice.SetText("0");
            craftingPrice.ForceMeshUpdate();


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
            IngredientData baseIngredient = selectBaseIngredientScreen.GetSelectedBaseIngredient();
            if(baseIngredient)
            {
                selectBaseIngredientButtonText.SetText(baseIngredient.ingredientName);
                selectBaseIngredientButtonText.ForceMeshUpdate();
                craftingItemData.baseIngredientID = baseIngredient.id;
            }
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
    }
}