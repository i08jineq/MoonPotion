using System.Collections;
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
        public TextMeshPro selectingBaseIngredientDescription;

        [Header("Prefab")]
        public BaseIngredientButton baseIngredientButtonPrefab;

        public Communicator onClosed = new Communicator();

        private BaseIngredientData selectedBaseIngredient;


        public void Setup()
        {
            closeButton.onClick.AddListener(CloseUI);
            selectButton.onClick.AddListener(CloseUI);

            CreateTape();
        }

        private void CreateTape()
        {
            List<int> ingredientTypes = Singleton.instance.currentSelectedSaveData.unlcokedBaseIngredientID;
            int number = ingredientTypes.Count;
            BaseIngredientButton cachedInstance = GameObject.Instantiate<BaseIngredientButton>(baseIngredientButtonPrefab, baseIngredientButtonRoot);
            cachedInstance.gameObject.SetActive(false);

            for (int i = 0; i < number; i++)
            {
                BaseIngredientData baseIngredient = Singleton.instance.resourceData.GetBaseIngredientData(ingredientTypes[i]);

                BaseIngredientButton button = GameObject.Instantiate<BaseIngredientButton>(cachedInstance, baseIngredientButtonRoot);
                button.Setup(baseIngredient);
                button.gameObject.SetActive(true);

                button.onSelected.AddListener(OnSelectedBaseIngredient);
            }

            if(cachedInstance != null)
            {
                GameObject.Destroy(cachedInstance.gameObject);
            }
        }

        public void ResetUI()
        {
            selectingBaseIngredientDescription.SetText("");
            selectingBaseIngredientDescription.ForceMeshUpdate();
            //reset all selecting
        }

        private void OnSelectedBaseIngredient(BaseIngredientData target)
        {
            selectedBaseIngredient = target;
        }

        private void CloseUI()
        {
            onClosed.Invoke();
        }

        public BaseIngredientData GetSelectedBaseIngredient()
        {
            return selectedBaseIngredient;
        }
    }
}