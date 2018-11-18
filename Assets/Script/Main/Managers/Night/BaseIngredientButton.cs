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

        [System.NonSerialized] public IngredientData target;
        public Communicator<IngredientData> onSelected = new Communicator<IngredientData>();

        public void Setup(IngredientData _targetIngredient)
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