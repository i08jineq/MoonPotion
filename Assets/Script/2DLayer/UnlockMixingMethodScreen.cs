using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class UnlockMixingMethodScreen : MonoBehaviour
    {
        public Button closeButton;
        public TextMeshProUGUI mixingMethodName;

        public Communicator onClosed = new Communicator();

        public void Setup()
        {
            closeButton.onClick.AddListener(OnClockedClose);
        }

        public void Open(MixingMethodType mixingMethodType)
        {
            gameObject.SetActive(true);
            mixingMethodName.SetText(mixingMethodType.ToString());
            mixingMethodName.ForceMeshUpdate();
        }

        private void OnClockedClose()
        {
            gameObject.SetActive(false);
            onClosed.Invoke();
        }
    }
}