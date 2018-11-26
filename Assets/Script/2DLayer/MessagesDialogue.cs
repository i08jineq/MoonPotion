using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DarkLordGame
{
    public class MessagesDialogue : MonoBehaviour
    {
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI messageText;
        public Button nextButton;
        public Button previousButton;
        public Button finishButton;

        public Communicator onFinished = new Communicator();

        private int currentMessageIndex = 0;
        private List<string> messagesList = new List<string>();

        public void Setup()
        {
            nextButton.onClick.AddListener(OnClickedNext);
            previousButton.onClick.AddListener(OnClickedPrevious);
            finishButton.onClick.AddListener(OnClickedFinishButton);
        }

        private void OnClickedFinishButton()
        {
            gameObject.SetActive(false);
            onFinished.Invoke();
        }

        private void OnClickedNext()
        {
            currentMessageIndex++;
            currentMessageIndex = Mathf.Min(currentMessageIndex, messagesList.Count - 1);
            UpdateUI();
        }

        private void OnClickedPrevious()
        {
            currentMessageIndex--;
            currentMessageIndex = Mathf.Max(0, currentMessageIndex);
            UpdateUI();
        }

        public void Open(List<string> messages, string title)
        {
            titleText.SetText(title);
            titleText.ForceMeshUpdate();

            messagesList.Clear();
            messagesList.AddRange(messages);

            currentMessageIndex = 0;
            UpdateUI();

            gameObject.SetActive(true);
        }

        private void UpdateUI()
        {
            int count = messagesList.Count;
            bool shouldShowFinishButton = currentMessageIndex == (count - 1);

            nextButton.gameObject.SetActive(!shouldShowFinishButton);
            finishButton.gameObject.SetActive(shouldShowFinishButton);

            bool shouldShowPreviousButton = currentMessageIndex > 0;
            previousButton.gameObject.SetActive(shouldShowPreviousButton);

            messageText.SetText(messagesList[currentMessageIndex]);
            messageText.ForceMeshUpdate();
        }
    }
}