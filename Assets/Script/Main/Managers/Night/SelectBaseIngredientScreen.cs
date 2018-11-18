using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DarkLordGame
{
    public class SelectBaseIngredientScreen : MonoBehaviour
    {
        public Button closeButton;
        public Button selectButton;
        public BaseIngredientButton baseIngredientButtonPrefab;

        public Communicator onClosed = new Communicator();

        private int selectinID = -1;

        public void Setup()
        {
            closeButton.onClick.AddListener(CloseUI);
            selectButton.onClick.AddListener(CloseUI);


        }

        private void CreateTape()
        {
            List<int> ingredientTypes = Singleton.instance.currentSelectedSaveData.unlcokedBaseIngredientID;
            int number = ingredientTypes.Count;
            //BaseIngredientButton cachedInstance = new 
            for (int i = 0; i < number; i++)
            {

            }
        }

        public void ResetUI()
        {
            selectinID = -1;
            //reset all selecting
        }

        private void CloseUI()
        {
            onClosed.Invoke();
        }

        public int GetSelectingID()
        {
            return selectinID;
        }


    }
}