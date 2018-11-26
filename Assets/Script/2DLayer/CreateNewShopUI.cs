using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DarkLordGame
{
    public class CreateNewShopUI : MonoBehaviour
    {
        public InputField shopNameField;
        public InputField playerNameField;
        public Button okButton;

        public Communicator onFinished = new Communicator();

        public void Setup()
        {
            shopNameField.onValueChanged.AddListener(OnNameChanged);
            playerNameField.onValueChanged.AddListener(OnNameChanged);

            okButton.interactable = false;
            okButton.onClick.AddListener(OnFinishedInput);
        }

        private void OnNameChanged(string changed)
        {
            bool isNotShopNameEmpty = shopNameField.text.Length != 0;
            bool isNotPlayerNameEmpty = playerNameField.text.Length != 0;
            okButton.interactable = isNotShopNameEmpty && isNotPlayerNameEmpty;
        }

        public void OnFinishedInput()
        {
            gameObject.SetActive(false);
            onFinished.Invoke();
        }
    }
}