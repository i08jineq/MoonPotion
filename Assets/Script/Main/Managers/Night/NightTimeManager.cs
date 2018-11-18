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
            craftNewItemScreen.uiEvent.AddListener(CraftNewItemUITopScreenEvent);
        }

        private void SetupCraftFromRecipeScreen()
        {
            craftItemFromRecipeScreen.Setup();
            craftItemFromRecipeScreen.gameObject.SetActive(false);
        }

        #region event

        public void Open()
        {
            craftNewItemScreen.gameObject.SetActive(false);
            craftNewItemScreen.ResetUI();

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
                    craftNewItemScreen.gameObject.SetActive(true);
                    break;
                case NightTimeUITopScreen.UIEvent.CraftFromRecipe:
                    craftItemFromRecipeScreen.gameObject.SetActive(true);
                    break;
            }
        }

        private void CraftNewItemUITopScreenEvent(CraftNewItemScreen.UIEvent eventType)
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