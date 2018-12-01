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
        public Vector3 goOutFromShopPosition;
        public Vector3 customerHomePosition;
        private InventoryItemData globalWanted;
        private List<CustomerPawn> allCustomers = new List<CustomerPawn>();
        private int customerCount = 0;

        public Communicator<int> onCustomerDonated = new Communicator<int>();
        public Communicator onAllCustomerVisited = new Communicator();

        public float totalWalkPeriod = 5;
        public float totalTurnPeriod = .3f;

        private const int basicDonation = 5;

        private float maxEffective = 0;
        private float maxTaste = 0;
        private float maxAbility = 0;
        private float maxTotalScore = 0;

        private int triggeringCustomerIndex = 0;
        private int entryNumbers = 0;
        private float trigerTimeCount = 0;
        private float customersActionPerTime = 2;
        private int arrivedHomeCustomerNumbers = 0;
        private float currentDeltaTime = 0;

        private List<IEnumerator> entryEnumerator = new List<IEnumerator>();

        public void Setup()
        {
            int numbers = Singleton.instance.resourceData.customersPawns.Length;
            for (int i = 0; i < numbers; i++)
            {
                GameObject customerObject = GameObject.Instantiate<GameObject>(Singleton.instance.resourceData.customersPawns[i]);
                CustomerPawn pawn = customerObject.GetComponent<CustomerPawn>();
                pawn.Setup();
                pawn.gameObject.SetActive(false);
                pawn.SetTransform(customersStartLocation, Vector3.forward);
                allCustomers.Add(pawn);
            }
            customerCount = numbers;
        }

        public void StandbyCustomers()
        {
            Updateparameters();
            UpdateCustomersEntry();
        }

        private void Updateparameters()
        {
            int inventory = Singleton.instance.saveData.inventoryItemDatas.Count;
            maxEffective = 0;
            maxTaste = 0;
            maxAbility = 0;
            maxTotalScore = 0;

            for (int i = 0; i < inventory; i++)
            {
                InventoryItemData inventoryItem = Singleton.instance.saveData.inventoryItemDatas[i];
                maxEffective = Mathf.Max(maxEffective, inventoryItem.effectiveScore);
                maxTaste = Mathf.Max(maxTaste, inventoryItem.tasteScore);
                maxAbility = Mathf.Max(maxAbility, inventoryItem.ability);
                float totalScore = (maxTaste + maxEffective + maxAbility) / 3;
                maxTotalScore = Mathf.Max(maxTotalScore, totalScore);
            }
        }

        private void UpdateCustomersEntry()
        {
            entryEnumerator.Clear();
            for (int i = 0; i < customerCount; i++)
            {
                CustomerPawn pawn = allCustomers[i];
                bool willCustomerDonate = false;
                float value = pawn.donateRequirement.value;
                switch (pawn.donateRequirement.donateRequirementType)
                {
                    case DonateRequirementType.AlwaysDonates:
                        willCustomerDonate = true;
                        break;
                    case DonateRequirementType.EveryParameter:
                        willCustomerDonate = maxEffective >= value && maxAbility >= value && maxTaste >= value;
                        break;
                    case DonateRequirementType.Ability:
                        willCustomerDonate = maxAbility >= value;
                        break;
                    case DonateRequirementType.Effect:
                        willCustomerDonate = maxEffective >= value;
                        break;
                    case DonateRequirementType.Taste:
                        willCustomerDonate = maxTaste >= value;
                        break;
                    case DonateRequirementType.TasteAndEffect:
                        willCustomerDonate = maxTaste >= value && maxEffective >= value;
                        break;
                    case DonateRequirementType.EffectAndAbility:
                        willCustomerDonate = maxAbility >= value && maxEffective >= value;
                        break;
                    case DonateRequirementType.TasteAndAbility:
                        willCustomerDonate = maxTaste >= value && maxEffective >= value;
                        break;
                }
                if (willCustomerDonate)
                {
                    entryEnumerator.Add(CustomerMovementEnumerator(pawn));
                }
                pawn.gameObject.SetActive(willCustomerDonate);

            }

            triggeringCustomerIndex = 0;
            trigerTimeCount = 0;
            entryNumbers = entryEnumerator.Count;
            arrivedHomeCustomerNumbers = 0;
        }

        public void OnUpdate(float deltaTime)
        {
            currentDeltaTime = deltaTime;
            TryActivateCustomer(deltaTime);
            UpdateEnumerator();
        }

        private void TryActivateCustomer(float deltaTime)
        {
            if(triggeringCustomerIndex >= entryNumbers)
            {
                return;
            }
            trigerTimeCount += deltaTime;
            if (trigerTimeCount >= customersActionPerTime)
            {
                triggeringCustomerIndex++;
            }
        }

        private void UpdateEnumerator()
        {
            for (int i = triggeringCustomerIndex - 1; i >= 0; i--)
            {
                if(entryEnumerator[i].MoveNext() == false)
                {
                    entryEnumerator.RemoveAt(i);
                }

            }
        }

        private IEnumerator CustomerMovementEnumerator(CustomerPawn pawn)
        {
            float t = 0;
            while(t <=totalWalkPeriod)
            {
                Vector3 position = Vector3.Lerp(customersStartLocation, shopQueue, t / totalWalkPeriod);
                pawn.SetTransform(position, Vector3.forward);
                t += currentDeltaTime;
                yield return null;
            }

            int donatePeriod = Random.Range(pawn.minDonates, pawn.maxDonates);
            onCustomerDonated.Invoke(donatePeriod);

            t = 0;
            while (t <= customersActionPerTime)
            {
                t += currentDeltaTime;
                yield return null;
            }

            t = 0;
            while(t <= totalTurnPeriod)
            {
                Vector3 position = Vector3.Lerp(shopQueue, goOutFromShopPosition, t / totalTurnPeriod);
                pawn.SetTransform(position, Vector3.right);
                t += currentDeltaTime;
                yield return null;
            }

            t = 0;
            while (t <= totalWalkPeriod)
            {
                Vector3 position = Vector3.Lerp(goOutFromShopPosition, customerHomePosition, t / totalWalkPeriod);
                pawn.SetTransform(position, Vector3.back);
                t += currentDeltaTime;
                yield return null;
            }

            OnCustomerArrivedHome();
        }

        private void OnCustomerArrivedHome()
        {
            arrivedHomeCustomerNumbers++;
            if(arrivedHomeCustomerNumbers>= entryNumbers)
            {
                onAllCustomerVisited.Invoke();
            }
        }
    }
}