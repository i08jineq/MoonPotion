﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class SelectBaseIngredientScreen : MonoBehaviour
    {
        [Header("Reference")]
        public Button closeButton;
        public Button selectButton;
        public Transform baseIngredientButtonRoot;
        public TextMeshProUGUI selectingBaseIngredientDescription;

        [Header("Prefab")]
        public BaseIngredientButton baseIngredientButtonPrefab;

        public Communicator onSelectedBaseIngredientChanged = new Communicator();
        public Communicator onClosed = new Communicator();
        private IngredientData selectedBaseIngredient;
        private List<BaseIngredientButton> baseIngredientButtons = new List<BaseIngredientButton>();
        public void Setup()
        {
            closeButton.onClick.AddListener(CloseUI);
            selectButton.onClick.AddListener(CloseUI);
            CreateBaseIngredientButton();
        }

        private void CreateBaseIngredientButton()
        {
            List<int> ingredientTypes = Singleton.instance.saveData.unlcokedBaseIngredientID;
            int number = ingredientTypes.Count;
            BaseIngredientButton cachedInstance = GameObject.Instantiate<BaseIngredientButton>(baseIngredientButtonPrefab, baseIngredientButtonRoot);
            cachedInstance.gameObject.SetActive(false);

            for (int i = 0; i < number; i++)
            {
                IngredientData baseIngredient = Singleton.instance.resourceData.GetBaseIngredientData(ingredientTypes[i]);
                BaseIngredientButton button = GameObject.Instantiate<BaseIngredientButton>(cachedInstance, baseIngredientButtonRoot);
                button.Setup(baseIngredient);
                button.gameObject.SetActive(true);
                button.onSelected.AddListener(OnSelectedBaseIngredient);
                baseIngredientButtons.Add(button);
            }

            GameObject.Destroy(cachedInstance.gameObject);
        }

        public BaseIngredientButton OnAddedNewBaseIngredient(IngredientData ingredient)
        {
            BaseIngredientButton button = GameObject.Instantiate<BaseIngredientButton>(baseIngredientButtonPrefab, baseIngredientButtonRoot);
            button.Setup(ingredient);
            button.gameObject.SetActive(true);
            button.onSelected.AddListener(OnSelectedBaseIngredient);
            baseIngredientButtons.Add(button);
            return button;
        }

        public void ResetUI()
        {
            selectingBaseIngredientDescription.SetText("No Item Selected");
            selectingBaseIngredientDescription.ForceMeshUpdate();

            selectedBaseIngredient = null;
        }

        private void OnSelectedBaseIngredient(IngredientData target)
        {
            selectedBaseIngredient = target;
            selectingBaseIngredientDescription.SetText(target.description);
            selectingBaseIngredientDescription.ForceMeshUpdate();

            int numbers = baseIngredientButtons.Count;
            for (int i = 0; i < numbers; i++)
            {
                if(baseIngredientButtons[i].target == target)
                {
                    continue;
                }
                baseIngredientButtons[i].SetNotSelected();
            }

            onSelectedBaseIngredientChanged.Invoke();
        }

        private void CloseUI()
        {
            gameObject.SetActive(false);
            onClosed.Invoke();
        }

        public IngredientData GetSelectedBaseIngredient()
        {
            return selectedBaseIngredient;
        }

    }
}