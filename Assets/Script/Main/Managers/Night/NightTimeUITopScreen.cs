using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DarkLordGame
{
    public class NightTimeUITopScreen : MonoBehaviour
    {
        public Button openShop;
        public Button craftNewItemButton;
        public Button craftFromRecipe;
        //public Button Research;

        public Communicator<UIEvent> uiEvent = new Communicator<UIEvent>();
        public enum UIEvent
        {
            Finish,
            CraftNewItem,
            CraftFromRecipe
        }

        public void Setup()
        {
            openShop.onClick.AddListener(OnClickedFinish);
            craftNewItemButton.onClick.AddListener(OnClickedCraftNewButton);
            craftFromRecipe.onClick.AddListener(OnClickedCraftFromRecipe);
        }

        #region uievent

        private void OnClickedFinish()
        {
            uiEvent.Invoke(UIEvent.Finish);
        }

        private void OnClickedCraftNewButton()
        {
            uiEvent.Invoke(UIEvent.CraftNewItem);
        }

        private void OnClickedCraftFromRecipe()
        {
            uiEvent.Invoke(UIEvent.CraftFromRecipe);
        }

        #endregion
    }
}