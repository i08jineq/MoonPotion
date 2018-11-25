using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "game event data", menuName = "DarkLordGame/GameEvent/Default")]
    public class GameEventData : ScriptableObject
    {
        public int eventID;
        public UnlockCondition condition;
        public List<UnlockData> unlockDataList = new List<UnlockData>();
    }
}