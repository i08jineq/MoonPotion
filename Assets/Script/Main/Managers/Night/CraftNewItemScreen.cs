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

        public TextMeshProUGUI selectBaseTypeButtonText;
        public TextMeshProUGUI selectIngredientButtonText;
        public TextMeshProUGUI selectMixingMethodButtonText;
        public TextMeshProUGUI craftingPrice;

        public SelectBaseIngredientScreen selectBaseIngredientScreen;
        public SelectIngredientScreen selectIngredientScreen;
        public SelectMixingMethodScreen selectMixingMethodScreen;

        public Communicator<UIEvent> uiEvent = new Communicator<UIEvent>();

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


        }

        //call everytime First try crafting
        public void ResetUI()
        {
            selectBaseTypeButtonText.SetText("Pick Base Ingredient");
            selectIngredientButtonText.SetText("Pick Ingredient");
            selectMixingMethodButtonText.SetText("Pick Mixing Method");

            selectBaseTypeButtonText.ForceMeshUpdate();
            selectIngredientButtonText.ForceMeshUpdate();
            selectMixingMethodButtonText.ForceMeshUpdate();

            selectBaseIngredientScreen.gameObject.SetActive(false);
            selectBaseIngredientScreen.ResetUI();
            selectIngredientScreen.gameObject.SetActive(false);
            selectIngredientScreen.ResetUI();
            selectMixingMethodScreen.gameObject.SetActive(false);
            selectMixingMethodScreen.ResetUI();

            craftingPrice.SetText("0");
            craftingPrice.ForceMeshUpdate();
            SetEnableCraftButton(false);
        }


        #region ui event

        public void OnClickedCancel()
        {
            uiEvent.Invoke(UIEvent.Cancel);
        }

        public void OnClickedCraft()
        {
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
    }
}