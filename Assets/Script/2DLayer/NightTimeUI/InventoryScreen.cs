using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class InventoryScreen : MonoBehaviour
    {
        public Button closeButton;
        public Transform craftedItemRoot;
        public CraftedItemUI craftedItemUIPrefab;
        public Communicator onClosed = new Communicator();

        public void Setup()
        {
            closeButton.onClick.AddListener(OnClickedCloseButton);

            CraftedItemUI craftedItem = GameObject.Instantiate<CraftedItemUI>(craftedItemUIPrefab, craftedItemRoot);
            int numbers = Singleton.instance.saveData.inventoryItemDatas.Count;
            for (int i = 0; i < numbers; i++)
            {
                CraftedItemUI cloned = GameObject.Instantiate<CraftedItemUI>(craftedItem, craftedItemRoot);
                cloned.SetData(Singleton.instance.saveData.inventoryItemDatas[i]);
            }
            GameObject.Destroy(craftedItem.gameObject);
        }

        public void OnAddedNewInventory(InventoryItemData inventoryItem)
        {
            CraftedItemUI cloned = GameObject.Instantiate<CraftedItemUI>(craftedItemUIPrefab, craftedItemRoot);
            cloned.SetData(inventoryItem);
        }

        #region ui event

        private void OnClickedCloseButton()
        {
            gameObject.SetActive(false);
            onClosed.Invoke();
        }

        #endregion
    }
}