using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class MixingMethodButton : MonoBehaviour
    {
        public MixingMethodType mixingMethodType = MixingMethodType.Boil;
        public TextMeshPro nameText;
        public Button button;
        public Communicator<MixingMethodType> onSelected = new Communicator<MixingMethodType>();

        public void Setup(MixingMethodType _mixingMethodType)
        {
            mixingMethodType = _mixingMethodType;
            nameText.SetText(mixingMethodType.ToString());
            nameText.ForceMeshUpdate();
            button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            button.image.color = button.colors.pressedColor;
            onSelected.Invoke(mixingMethodType);
        }

        public void SetUnSelected()
        {
            button.image.color = button.colors.normalColor;
        }
    }
}