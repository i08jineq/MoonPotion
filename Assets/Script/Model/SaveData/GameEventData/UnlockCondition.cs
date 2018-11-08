using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public struct UnlockCondition
    {
        public UnlockConditionType unlockConditionType;
        public List<int> dataParameter;
    }
}