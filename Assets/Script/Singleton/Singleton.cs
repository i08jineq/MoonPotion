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
        public SaveData saveData = new SaveData();

        public const int maxSlotNumber = 4;
        public int selectedSlotIndex = 0;

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
                instance.LoadSavedSlotIndex();
                int numbers = instance.savedSlotIndex.Count;
                for (int i = 0; i < numbers; i++)
                {
                    SaveData saveDatas = instance.LoadSaveData(instance.savedSlotIndex[i]);
                    instance.allSaveData.Add(saveDatas);
                    yield return null;
                }

            }
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
            saveData = PersistenceData.LoadData<SaveData>("slot_" + slotIndex.ToString(), new SaveData());
            return saveData;
        }

        public void SaveData(int slotIndex)
        {
            PersistenceData.SaveData<SaveData>("slot_" + slotIndex.ToString(), saveData);
        }
        #endregion
    }
}