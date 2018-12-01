using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DarkLordGame
{
    public class TopPanelUI : MonoBehaviour
    {
        public GameObject dayTextRoot;
        public TextMeshProUGUI dayText;
        public TextMeshProUGUI goldAmount;
        public TextMeshProUGUI shopName;

        public Button pauseButton;
        public Button normalSpeedButton;
        public Button fastButton;
        public Button superFastButton;

        public void SetGoldAmount(int gold)
        {
            goldAmount.SetText(gold.ToString());
            goldAmount.ForceMeshUpdate();
        }

        public void SetPlaySpeedActive(Button button)
        {
            normalSpeedButton.image.color = button == normalSpeedButton ? normalSpeedButton.colors.pressedColor : normalSpeedButton.colors.normalColor;
            fastButton.image.color = button == fastButton ? fastButton.colors.pressedColor : fastButton.colors.normalColor;
            superFastButton.image.color = button == superFastButton ? superFastButton.colors.pressedColor : superFastButton.colors.normalColor;
        }

        public void SetName(string _shopName)
        {
            shopName.SetText(_shopName);
        }
        public void SetDay(int day)
        {
            dayTextRoot.SetActive(day != 0);
            dayText.SetText(day.ToString());
            dayText.ForceMeshUpdate();
        }
    }
}