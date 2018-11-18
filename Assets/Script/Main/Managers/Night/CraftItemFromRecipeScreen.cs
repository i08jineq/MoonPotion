using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class CraftItemFromRecipeScreen : MonoBehaviour
    {
        public Button cancelButton;
        public Button craftButton;
        public Button deleteRecipeButton;
        //select recipe UI
        public TextMeshProUGUI numberText;
        public Button plusNumberButton;
        public Button minusNumberButton;

        public GameObject askConfirmDelete;
        public Button cancelDeleteButton;
        public Button confirmDeleteButton;

        public Communicator<UIEvent> uiEvent = new Communicator<UIEvent>();

        private int selectingRecipeId = 0;
        private int selectingNumber = 1;

        public enum UIEvent
        {
            Cancel,
            Craft,
            DeleteRecipe,
        }

        public void Setup()
        {
            plusNumberButton.onClick.AddListener(OnClickedPlusButton);
            craftButton.onClick.AddListener(OnClickedCraftButton);

            deleteRecipeButton.onClick.AddListener(OnClickedDeleteRecipeButton);
            confirmDeleteButton.onClick.AddListener(OnClickedConfirmDeleteButton);
            cancelDeleteButton.onClick.AddListener(OnClickedCancelDeleteRecipe);

            minusNumberButton.onClick.AddListener(OnClickedMinusButton);
            cancelButton.onClick.AddListener(OnClickedCancelButton);

            //select recipe
            //show recipe details
            //update can craft
        }
        #region ui event
        private void OnClickedPlusButton()
        {
            selectingNumber--;
            selectingNumber = Mathf.Min(10, selectingNumber);
            UpdateNumberText();
            UpdateCraftEnableState();
        }

        private void OnClickedMinusButton()
        {
            selectingNumber--;
            selectingNumber = Mathf.Max(1, selectingNumber);
            UpdateNumberText();
            UpdateCraftEnableState();
        }

        private void OnClickedCancelButton()
        {
            uiEvent.Invoke(UIEvent.Cancel);
        }

        private void OnClickedCraftButton()
        {
            uiEvent.Invoke(UIEvent.Craft);
        }

        private void OnClickedDeleteRecipeButton()
        {
            askConfirmDelete.gameObject.SetActive(true);
        }

        private void OnClickedConfirmDeleteButton()
        {
            uiEvent.Invoke(UIEvent.DeleteRecipe);
            askConfirmDelete.gameObject.SetActive(false);
        }

        private void OnClickedCancelDeleteRecipe()
        {
            askConfirmDelete.gameObject.SetActive(false);
        }

        #endregion

        private void UpdateCraftEnableState()
        {
            //price * money <= have money
            bool canCraftWithRecipe = false;
            craftButton.interactable = canCraftWithRecipe;
        }

        private void UpdateNumberText()
        {
            numberText.SetText(selectingNumber.ToString());
        }

        public int GetSelectingRecipeId()
        {
            return selectingRecipeId;
        }

        public int GetSelectingNumber()
        {
            return selectingNumber;
        }
    }
}