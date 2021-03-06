﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DarkLordGame
{
    public class SelectIngredientScreen : MonoBehaviour
    {
        public IngredientButton ingredientButtonPrefab;
        public Button closeButton;
        public Transform ingreDientButtonRoot;

        private List<IngredientData> selectingIngredientDatas = new List<IngredientData>();
        private const int selectAbleIngredientNumber = 3;
        public Communicator onFinished = new Communicator();
        public Communicator onSelectingIngredientChanged = new Communicator();

        private List<IngredientButton> ingredientButtonList = new List<IngredientButton>();
       
        public void Setup()
        {
            SetupIngredientsButton();
            SetupCloseButton();
        }

        private void SetupIngredientsButton()
        {
            List<int> ingredientIDs = Singleton.instance.saveData.unlcokedIngredientID;
            int number = ingredientIDs.Count;
            IngredientButton cahcedIngredientButton = GameObject.Instantiate<IngredientButton>(ingredientButtonPrefab, ingreDientButtonRoot);

            for (int i = 0; i < number; i++)
            {
                IngredientButton ingredientButton = GameObject.Instantiate<IngredientButton>(cahcedIngredientButton, ingreDientButtonRoot);
                IngredientData ingredientData = Singleton.instance.resourceData.GetIngredient(ingredientIDs[i]);
                ingredientButton.Setup(ingredientData);
                ingredientButton.checkBox.isOn = false;
                ingredientButton.onSelected.AddListener(OnSelected);
                ingredientButtonList.Add(ingredientButton);
            }
            GameObject.Destroy(cahcedIngredientButton.gameObject);
        }

        public IngredientButton OnAddIngredient(IngredientData ingredientData)
        {
            IngredientButton ingredientButton = GameObject.Instantiate<IngredientButton>(ingredientButtonPrefab, ingreDientButtonRoot);
            ingredientButton.Setup(ingredientData);
            ingredientButton.onSelected.AddListener(OnSelected);
            ingredientButton.checkBox.isOn = false;
            ingredientButtonList.Add(ingredientButton);

            return ingredientButton;
        }

        private void SetupCloseButton()
        {
            closeButton.onClick.AddListener(OnFinished);
        }

        private void OnFinished()
        {
            gameObject.SetActive(false);
            onFinished.Invoke();
        }

        private void OnSelected(bool selected, IngredientData ingredientData)
        {
            if(selected)
            {
                selectingIngredientDatas.Add(ingredientData);
            }else
            {
                selectingIngredientDatas.Remove(ingredientData);
            }
            onSelectingIngredientChanged.Invoke();
        }

        public IngredientData[] GetIngredientDatas()
        {
            return selectingIngredientDatas.ToArray();
        }

        public void ResetUI()
        {
            selectingIngredientDatas.Clear();
            int count = ingredientButtonList.Count;
            for (int i = 0; i < count; i++)
            {
                ingredientButtonList[i].UncheckCheckbox();
            }
        }
    }
}