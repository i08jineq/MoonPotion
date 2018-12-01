using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkLordGame
{
    [System.Serializable]
    public class CustomersManager
    {
        public Vector3 customersStartLocation;
        public Vector3 shopQueue;
        private InventoryItemData globalWanted;
        private List<CustomerPawn> allCustomers = new List<CustomerPawn>();
        private List<CustomerPawn> shopEntry = new List<CustomerPawn>();

        public Communicator<CustomerPawn, int> onCustomerTryToBuyItem = new Communicator<CustomerPawn, int>();
        public float totalWalkPeriod = 1;

        public void Setup()
        {
            int numbers = Singleton.instance.resourceData.customersPawns.Length;
            for (int i = 0; i < numbers; i++)
            {
                GameObject customerObject = GameObject.Instantiate<GameObject>(Singleton.instance.resourceData.customersPawns[i]);
                CustomerPawn pawn = customerObject.GetComponent<CustomerPawn>();
                pawn.Setup();
                allCustomers.Add(pawn);
            }
        }

        public void StandbyCustomers()
        {
            //setup customers needs
        }

        public bool AllCustomersBoughtItem()
        {
            return shopEntry.Count == 0;
        }
    }
}