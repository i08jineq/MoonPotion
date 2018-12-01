using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "Mixing method unlock", menuName = "DarkLordGame/UnlockData/MixingMethod")]
    public class MixingMethodUnlockData : UnlockData
    {
        public MixingMethodType targetMixingMethodType;

        public override UnlockDataType GetUnlockType()
        {
            return UnlockDataType.MixMethod;
        }
    }
}