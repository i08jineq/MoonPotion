using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class Singleton
    {
        public static Singleton instance;
        public Events events = new Events();//clear at the end of level
        public List<int> savedSlotIndex = new List<int>();
        public List<SaveData> allSaveData = new List<SaveData>();

        public SaveData currentSelectedSaveData = new SaveData();
        public ResourceData resourceData = new ResourceData();

        public const int maxSlotNumber = 4;
        public int selectedSlotIndex = 0;

        public Canvas mainCanvas;
        public Transform mainCanvasTransform;

        private const string saveSlotDataName = "savedslot";

        public Singleton()
        {
            events = new Events();
        }

        public static IEnumerator Init()
        {
            if (instance == null)
            {
                instance = new Singleton();
                yield return instance.resourceData.LoadResource();

                instance.LoadSavedSlotIndex();

                int numbers = instance.savedSlotIndex.Count;
                for (int i = 0; i < numbers; i++)
                {
                    SaveData saveDatas = instance.LoadSaveData(instance.savedSlotIndex[i]);
                    instance.allSaveData.Add(saveDatas);
                    yield return null;
                }
                if(instance.currentSelectedSaveData == null)
                {
                    instance.currentSelectedSaveData = new SaveData();
                }
            }

            instance.mainCanvas = GameObject.FindObjectOfType<Canvas>();
            instance.mainCanvasTransform = instance.mainCanvas.transform;
        }

        #region savesdata
        private void LoadSavedSlotIndex()
        {
            savedSlotIndex = PersistenceData.LoadData<List<int>>(saveSlotDataName, new List<int>());
        }

        public void OnCreateNewSlotData(int slotIndex)
        {
            if (savedSlotIndex.Contains(slotIndex) == false)
            {
                savedSlotIndex.Add(slotIndex);
            }

            PersistenceData.SaveData<List<int>>(saveSlotDataName, savedSlotIndex);
            PersistenceData.SaveData<SaveData>("slot_" + slotIndex.ToString(), new SaveData());
        }

        public SaveData LoadSaveData(int slotIndex)
        {
            currentSelectedSaveData = PersistenceData.LoadData<SaveData>("slot_" + slotIndex.ToString(), new SaveData());
            return currentSelectedSaveData;
        }

        public void SaveData(int slotIndex)
        {
            PersistenceData.SaveData<SaveData>("slot_" + slotIndex.ToString(), currentSelectedSaveData);
        }
        #endregion
    }
}