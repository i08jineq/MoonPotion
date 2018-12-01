using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DarkLordGame
{
    public class CraftNewItemScreen : MonoBehaviour
    {
        public Button craftButton;
        public Button cancelButton;

        public GameObject topScreenRoot;
        public Button selectBaseButton;
        public Button selectIngredientButton;
        public Button selectMixingMethodButton;
        public InputField itemName;

        public TextMeshProUGUI selectBaseIngredientButtonText;
        public TextMeshProUGUI selectIngredientButtonText;
        public TextMeshProUGUI selectMixingMethodButtonText;
        public TextMeshProUGUI craftingPrice;


        public SelectBaseIngredientScreen selectBaseIngredientScreen;
        public SelectIngredientScreen selectIngredientScreen;
        public SelectMixingMethodScreen selectMixingMethodScreen;

        public Communicator<UIEvent> uiEvent = new Communicator<UIEvent>();

        private InventoryItemData craftingItemData = new InventoryItemData();
        private int baseIngredientPrice = 0;
        private int ingredientsSumPrice = 0;

        [System.NonSerialized]public IngredientData currentBaseIngredientData;
        [System.NonSerialized]public List<IngredientData> currentIngredients = new List<IngredientData>();

        private const string BaseIngredientEmptyText = "Pick Base Ingredient";
        private const string IngredientEmptyText = "Pick Ingredient";
        private const string MixthingMethodEmptyText = "Pick Mixing Method";

        public enum UIEvent
        {
            Cancel,
            Craft
        }

        public void Setup()
        {
            cancelButton.onClick.AddListener(OnClickedCancel);
            craftButton.onClick.AddListener(OnClickedCraft);
            selectBaseButton.onClick.AddListener(OnclickedSelectBase);
            selectIngredientButton.onClick.AddListener(OnClickedSelectIngredient);
            selectMixingMethodButton.onClick.AddListener(OnClickedMixingMethod);

            selectBaseIngredientScreen.Setup();
            selectBaseIngredientScreen.onClosed.AddListener(UpdateCraftButtonEnable);
            selectBaseIngredientScreen.onClosed.AddListener(OpenTopScreen);

            selectBaseIngredientScreen.onSelectedBaseIngredientChanged.AddListener(UpdatePrice);
            selectBaseIngredientScreen.onSelectedBaseIngredientChanged.AddListener(UpdateBaseIngredient);

            selectIngredientScreen.Setup();
            selectIngredientScreen.onFinished.AddListener(UpdateCraftButtonEnable);
            selectIngredientScreen.onFinished.AddListener(OpenTopScreen);

            selectIngredientScreen.onSelectingIngredientChanged.AddListener(UpdatePrice);
            selectIngredientScreen.onSelectingIngredientChanged.AddListener(UpdateIngredient);

            selectMixingMethodScreen.Setup();
            selectMixingMethodScreen.onChangedMixingMethod.AddListener(UpdateMixingMethod);
            selectMixingMethodScreen.onClosed.AddListener(UpdateCraftButtonEnable);
            selectMixingMethodScreen.onClosed.AddListener(OpenTopScreen);
        }

        //call everytime First try crafting
        public void ResetUI()
        {
            selectBaseIngredientButtonText.SetText(BaseIngredientEmptyText);
            selectIngredientButtonText.SetText(IngredientEmptyText);
            selectMixingMethodButtonText.SetText(MixthingMethodEmptyText);

            selectBaseIngredientButtonText.ForceMeshUpdate();
            selectIngredientButtonText.ForceMeshUpdate();
            selectMixingMethodButtonText.ForceMeshUpdate();

            selectBaseIngredientScreen.ResetUI();
            selectIngredientScreen.ResetUI();
            selectMixingMethodScreen.ResetUI();

            itemName.text = "";
            itemName.onValueChanged.AddListener(OnNameChanged);

            craftingPrice.SetText("0");
            craftingPrice.ForceMeshUpdate();
            baseIngredientPrice = 0;
            ingredientsSumPrice = 0;
            craftingItemData = new InventoryItemData();
            craftingItemData.baseIngredientID = -1;

            SetEnableCraftButton(false);
        }

        public void Open()
        {
            selectBaseIngredientScreen.gameObject.SetActive(false);
            selectIngredientScreen.gameObject.SetActive(false);
            selectMixingMethodScreen.gameObject.SetActive(false);
            OpenTopScreen();
            gameObject.SetActive(true);
        }

        #region ui event

        public void OnClickedCancel()
        {
            gameObject.SetActive(false);
            uiEvent.Invoke(UIEvent.Cancel);
        }

        public void OnClickedCraft()
        {
            gameObject.SetActive(false);

            uiEvent.Invoke(UIEvent.Craft);
        }

        public void OnclickedSelectBase()
        {
            selectBaseIngredientScreen.gameObject.SetActive(true);
            SetactiveTopScreenUI(false);
        }

        public void OnClickedSelectIngredient()
        {
            selectIngredientScreen.gameObject.SetActive(true);
            SetactiveTopScreenUI(false);
        }

        public void OnClickedMixingMethod()
        {
            selectMixingMethodScreen.gameObject.SetActive(true);
            SetactiveTopScreenUI(false);
        }

        private void OpenTopScreen()
        {
            SetactiveTopScreenUI(true);
        }

        private void UpdateBaseIngredient()
        {
            IngredientData baseIngredient = selectBaseIngredientScreen.GetSelectedBaseIngredient();
            currentBaseIngredientData = baseIngredient;
            if (baseIngredient != null)
            {
                selectBaseIngredientButtonText.SetText(baseIngredient.ingredientName);
                selectBaseIngredientButtonText.ForceMeshUpdate();
                craftingItemData.baseIngredientID = baseIngredient.id;
                baseIngredientPrice = baseIngredient.price;
            }else
            {
                selectBaseIngredientButtonText.SetText(BaseIngredientEmptyText);
                selectBaseIngredientButtonText.ForceMeshUpdate();
                baseIngredientPrice = 0;
            }
        }

        private void UpdateIngredient()
        {
            IngredientData[] ingredients = selectIngredientScreen.GetIngredientDatas();
            currentIngredients.Clear();
            currentIngredients.AddRange(ingredients);

            int length = ingredients.Length;
            ingredientsSumPrice = 0;
            craftingItemData.ingredientIDs.Clear();
            for (int i = 0; i < length; i++)
            {
                craftingItemData.ingredientIDs.Add(ingredients[i].id);
                ingredientsSumPrice += ingredients[i].price;
            }
            if(length == 0)
            {
                selectIngredientButtonText.SetText(IngredientEmptyText);
                selectIngredientButtonText.ForceMeshUpdate();
                return;
            }
            selectIngredientButtonText.SetText("Selected " + (length));
            selectIngredientButtonText.ForceMeshUpdate();
        }

        private void UpdateMixingMethod(MixingMethodType mixingMethodType)
        {
            craftingItemData.mixingMethod = mixingMethodType;
            selectMixingMethodButtonText.SetText(mixingMethodType.ToString());
            selectMixingMethodButtonText.ForceMeshUpdate();
        }

        private void UpdatePrice()
        {
            craftingPrice.SetText(GetTotalPrice().ToString());
            craftingPrice.ForceMeshUpdate();
        }

        private void UpdateCraftButtonEnable()
        {
            bool hasAnyItem = craftingItemData.ingredientIDs.Count > 0;
            bool hasBaseItem = craftingItemData.baseIngredientID != -1;
            bool selectedMethod = craftingItemData.mixingMethod != MixingMethodType.None;
            bool hasName = craftingItemData.itemName.Length > 0;
            bool hasEnoughMoney = Singleton.instance.saveData.currentGold >= GetTotalPrice();

            SetEnableCraftButton(hasBaseItem && hasAnyItem && selectedMethod && hasName && hasEnoughMoney);
        }

        private void OnNameChanged(string _itemName)
        {
            craftingItemData.itemName = _itemName;
            UpdateCraftButtonEnable();
        }

        #endregion

        private void SetactiveTopScreenUI(bool activate)
        {
            craftButton.gameObject.SetActive(activate);
            cancelButton.gameObject.SetActive(activate);
            topScreenRoot.SetActive(activate);
            itemName.gameObject.SetActive(activate);
        }

        public void SetEnableCraftButton(bool interactable)
        {
            craftButton.interactable = interactable;
        }

        public InventoryItemData GetCraftingItemData()
        {
            return craftingItemData;
        }

        public int GetTotalPrice()
        {
            return baseIngredientPrice + ingredientsSumPrice;
        }
    }
}