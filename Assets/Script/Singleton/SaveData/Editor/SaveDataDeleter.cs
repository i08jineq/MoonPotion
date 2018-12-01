using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DarkLordGame
{
    public static class SaveDataDeleter
    {

        [MenuItem("DarkLordGame/DeleteSave")]
        public static void DeleteSaveData()
        {
            for (int i = 0; i < Singleton.maxSlotNumber; i++)
            {
                PersistenceData.DeleteFile("savedslot");
            }

        }
    }
}