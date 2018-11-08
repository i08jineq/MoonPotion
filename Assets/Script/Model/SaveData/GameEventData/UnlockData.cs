using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public struct UnlockData
    {
        public UnlockDataType unlockDataType;
        public int unlockId;
        public int amount;
    }
}