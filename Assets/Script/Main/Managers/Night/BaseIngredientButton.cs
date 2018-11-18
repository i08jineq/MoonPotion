using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class BaseIngredientButton : MonoBehaviour
    {
        public TextMeshProUGUI itemNameText;
        public Button button;

        [System.NonSerialized] public BaseIngredientData baseIngredientType;
        public Communicator<BaseIngredientData> onSelected = new Communicator<BaseIngredientData>();

        public void Setup(BaseIngredientData _targetType)
        {
            baseIngredientType = _targetType;
            button.onClick.AddListener(OnClcikedButton);
        }

        private void OnClcikedButton()
        {
            onSelected.Invoke(baseIngredientType);
        }
    }
}