using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class GameEventManager
    {
        // ui of events
        public CreateNewShopUI createNewShopUI;
        public MessagesDialogue messageDialogueUI;
        public UnlockIngredientScreen unlockIngredientScreen;

        [System.NonSerialized]public Communicator onFinishedExecuteEvent = new Communicator();
        private List<GameEventData> targetEventData = new List<GameEventData>();
        private GameEventData currentEventData;
        private List<UnlockData> unlockDatas = new List<UnlockData>();
        private int currentShowIndex = 0;
        private int unlockDataNumber = 0;

        public Communicator onFinishedAllEvent = new Communicator();

        #region setup

        public void Setup()
        {
            PreloadTargetEvent();
            SetupUI();
        }

        private void PreloadTargetEvent()
        {
            List<int> completedID = Singleton.instance.saveData.completedEventID;
            int number = completedID.Count;
            targetEventData.AddRange(Singleton.instance.resourceData.gameEventDatas);
            int totalNumber = Singleton.instance.resourceData.gameEventDatasNumbers;

            for (int i = totalNumber - 1; i >= 0; i--)
            {
                if (completedID.Contains(targetEventData[i].eventID))
                {
                    targetEventData.RemoveAt(i);
                }
            }
        }

        private void SetupUI()
        {
            createNewShopUI.Setup();
            createNewShopUI.onFinished.AddListener(OnFinishedInputShopName);
            createNewShopUI.onFinished.AddListener(OnFinishedEvent);
            createNewShopUI.gameObject.SetActive(false);

            messageDialogueUI.Setup();
            messageDialogueUI.onFinished.AddListener(OnFinishedEvent);
            messageDialogueUI.gameObject.SetActive(false);

            unlockIngredientScreen.Setup();
            unlockIngredientScreen.onClosed.AddListener(OnFinishedEvent);
        }

        #endregion

        public void TryStartDayEvent(int day)
        {
            currentEventData = GetEventDataType(UnlockConditionType.StartDay, day);
            if(currentEventData == null)
            {
                onFinishedAllEvent.Invoke();
                return;
            }

            ExecuteCurrentUnlockDataFromBegin();
        }

        public void TryUnlockIngredient(int craftNumber)
        {
            currentEventData = GetEventDataType(UnlockConditionType.CraftedNumber, craftNumber);
            if (currentEventData == null)
            {
                return;
            }
            ExecuteCurrentUnlockDataFromBegin();
        }

        public GameEventData GetEventDataType(UnlockConditionType targetType, int value)
        {
            int count = targetEventData.Count;
            for (int i = 0; i < count; i++)
            {
                if (targetEventData[i].condition.unlockConditionType == targetType && targetEventData[i].condition.conditionValue == value)
                {
                    return targetEventData[i];
                }
            }
            return null;
        }

        private void OnFinishedInputShopName()
        {
            Singleton.instance.saveData.shopName = createNewShopUI.shopNameField.text;
            Singleton.instance.saveData.playername = createNewShopUI.playerNameField.text;
            Singleton.instance.SaveData();
        }

        private void OnFinishedEvent()
        {
            currentShowIndex++;
            if(currentShowIndex >= unlockDataNumber)
            {
                onFinishedAllEvent.Invoke();
                targetEventData.Remove(currentEventData);
                Singleton.instance.saveData.completedEventID.Add(currentEventData.eventID);
                Singleton.instance.SaveData();
                return;
            }
            ExecuteCurrentUnlockDataEvent();
        }

        private void ExecuteCurrentUnlockDataFromBegin()
        {
            unlockDatas.Clear();
            unlockDatas = currentEventData.unlockDataList;
            currentShowIndex = 0;
            unlockDataNumber = unlockDatas.Count;
            ExecuteCurrentUnlockDataEvent();
        }

        private void ExecuteCurrentUnlockDataEvent()
        {
            switch (unlockDatas[currentShowIndex].GetUnlockType())
            {
                case UnlockDataType.CreateNewShop:
                    createNewShopUI.gameObject.SetActive(true);
                    break;
                case UnlockDataType.Tutorial:
                    TutorialUnlockData tutorial = unlockDatas[currentShowIndex] as TutorialUnlockData;
                    messageDialogueUI.Open(tutorial.tutorialMessages, "-- Tips --");
                    break;

                case UnlockDataType.Ingredient:
                    IngredientUnlockData ingredientUnlock = unlockDatas[currentShowIndex] as IngredientUnlockData;
                    unlockIngredientScreen.Open(ingredientUnlock.targetIngredientData);
                    break;
            }
        }
    }
}