using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DarkLordGame
{
    public class SelectBaseIngrediantScreen : MonoBehaviour
    {
        public Button closeButton;
        public Button selectButton;
        public Communicator onClosed = new Communicator();
        private int selectinID = -1;

        public void Setup()
        {
            closeButton.onClick.AddListener(CloseUI);
            selectButton.onClick.AddListener(CloseUI);
        }

        private void CloseUI()
        {
            onClosed.Invoke();
        }

        public void ResetUI()
        {
            selectinID = -1;
            //reset all selecting
        }

        public int GetSelectingID()
        {
            return selectinID;
        }


    }
}