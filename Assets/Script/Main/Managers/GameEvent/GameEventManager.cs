using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class GameEventManager
    {
        // ui of events
        public MessagesDialogue messageDialogueUI;
        [System.NonSerialized]public Communicator onFinishedExecuteEvent = new Communicator();
        private List<GameEventData> targetEventData = new List<GameEventData>();

        #region setup

        public void Setup()
        {
            PreloadTargetEvent();
            SetupUI();
        }

        private void PreloadTargetEvent()
        {
            List<int> completedID = Singleton.instance.currentSelectedSaveData.completedEventID;
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
            messageDialogueUI.Setup();
        }

        #endregion

        public GameEventData GetEventDataDay(int day)
        {
            int count = targetEventData.Count;
            for (int i = 0; i < count; i++)
            {
                if (targetEventData[i].condition.unlockConditionType == UnlockConditionType.StartDay && targetEventData[i].condition.conditionValue == day)
                {
                    return targetEventData[i];
                }
            }
            return null;
        }

    }
}