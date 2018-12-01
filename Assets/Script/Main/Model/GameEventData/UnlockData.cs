using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{

    public abstract class UnlockData : ScriptableObject
    {
        public abstract UnlockDataType GetUnlockType();
    }
}