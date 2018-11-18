using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class GameEventData : ScriptableObject
    {
        public int eventID;
        public List<UnlockCondition> condition = new List<UnlockCondition>();
        public List<UnlockData> unlockData = new List<UnlockData>();
    }
}