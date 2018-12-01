using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class Singleton
    {
        public static Singleton instance;

        public SaveData currentSelectedSaveData = new SaveData();
        public ResourceData resourceData = new ResourceData();

        public const int maxSlotNumber = 4;
        public int selectedSlotIndex = 0;

        public Canvas mainCanvas;
        public Transform mainCanvasTransform;

        private const string saveSlotDataName = "savedslot";

        public static IEnumerator Init()
        {
            if (instance == null)
            {
                instance = new Singleton();
                yield return instance.resourceData.LoadResources();

                instance.LoadSaveData();

            }

            instance.mainCanvas = GameObject.FindObjectOfType<Canvas>();
            instance.mainCanvasTransform = instance.mainCanvas.transform;
        }

        #region savesdata

        public SaveData LoadSaveData()
        {
            currentSelectedSaveData = PersistenceData.LoadData<SaveData>(saveSlotDataName, new SaveData());
            return currentSelectedSaveData;
        }

        public void SaveData()
        {
            PersistenceData.SaveData<SaveData>(saveSlotDataName, currentSelectedSaveData);
        }

        #endregion
    }
}