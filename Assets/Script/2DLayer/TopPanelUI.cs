using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DarkLordGame
{
    public class TopPanelUI : MonoBehaviour
    {
        public TextMeshProUGUI goldAmount;
        public Button pauseButton;

        public void SetGoldAmount(int gold)
        {
            goldAmount.SetText(gold.ToString());
            goldAmount.ForceMeshUpdate();
        }
    }
}