using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DarkLordGame
{
    public class CraftedItemUI : MonoBehaviour
    {
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI effective;
        public TextMeshProUGUI taste;
        public TextMeshProUGUI ability;

        public void SetData(InventoryItemData inventoryItem)
        {
            itemName.SetText(inventoryItem.itemName);
            effective.SetText(inventoryItem.effectiveScore.ToString("n0"));
            taste.SetText(inventoryItem.tasteScore.ToString("n0"));
            ability.SetText(inventoryItem.ability.ToString("n0"));

            itemName.ForceMeshUpdate();
            effective.ForceMeshUpdate();
            taste.ForceMeshUpdate();
            ability.ForceMeshUpdate();
        }
    }
}