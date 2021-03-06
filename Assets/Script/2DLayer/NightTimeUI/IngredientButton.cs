﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class IngredientButton : MonoBehaviour
    {
        public Toggle checkBox;
        public TextMeshProUGUI ingredientName;

        private IngredientData targetIngredient;

        public Communicator<bool, IngredientData> onSelected = new Communicator<bool, IngredientData>();

        public void Setup(IngredientData _targetIngredient)
        {
            targetIngredient = _targetIngredient;
            ingredientName.SetText(_targetIngredient.ingredientName);
            ingredientName.ForceMeshUpdate();
            checkBox.onValueChanged.AddListener(OnCheckBoxVolumeChanged);
        }

        public void UncheckCheckbox()
        {
            checkBox.isOn = false;
        }

        private void OnCheckBoxVolumeChanged(bool value)
        {
            onSelected.Invoke(value, targetIngredient);
        }

        public void SetActivable(bool activeable)
        {
            checkBox.interactable = activeable;
        }
    }
}