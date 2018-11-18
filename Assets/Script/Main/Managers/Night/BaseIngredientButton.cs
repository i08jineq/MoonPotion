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

        [System.NonSerialized] public BaseIngredientData target;
        public Communicator<BaseIngredientData> onSelected = new Communicator<BaseIngredientData>();

        public void Setup(BaseIngredientData _targetIngredient)
        {
            target = _targetIngredient;

            itemNameText.SetText(_targetIngredient.ingredientName);
            itemNameText.ForceMeshUpdate();

            button.onClick.AddListener(OnClcikedButton);
        }

        private void OnClcikedButton()
        {
            onSelected.Invoke(target);
        }
    }
}