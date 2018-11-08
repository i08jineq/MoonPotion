using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class Model
    {
        public static Model instance;
        public GameEvent gameEvent = new GameEvent();//clear at the end of level
        public SaveData saveData = new SaveData();

        public int selectedSlotIndex = 0;

        public Model()
        {
            gameEvent = new GameEvent();
        }

        public static void Init()
        {
            if (instance == null)
            {
                instance = new Model();
            }
        }


        public void LoadSaveData(int slotIndex)
        {
            saveData = PersistenceData.LoadData<SaveData>("slot_" + slotIndex.ToString() + "saveName", new SaveData());
        }
    }
}