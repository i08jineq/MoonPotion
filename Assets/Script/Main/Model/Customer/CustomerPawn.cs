using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class CustomerPawn : MonoBehaviour
    {
        [System.NonSerialized] public bool boughtItem = false;
        [System.NonSerialized] public bool hasWantedItem = false;
        [System.NonSerialized] public int wantedItemID = 0;
        [Range(0, 5)]public float startTime;
        public InventoryItemData wantedItem = new InventoryItemData();
        private Transform localTransform;


        public void Setup()
        {
            localTransform = transform;
        }

        public void SetTransform(Vector3 position, Vector3 forward)
        {
            localTransform.position = position;
            localTransform.forward = forward;
        }
    }

}