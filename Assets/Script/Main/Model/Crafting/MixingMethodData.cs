using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "MixingMethod", menuName ="DarkLordGame/MixingMethod")]
    public class MixingMethodData : ScriptableObject
    {
        [System.Serializable]
        public class MixingMethodMultiplier
        {
            public MixingMethodType mixingMethod;
            public float effectiveMultiplier = 1;
            public float tasteMultiplier = 1;
            public float abilityMultiplier = 1;
        }

        public List<MixingMethodMultiplier> mixingMethodMultipliers = new List<MixingMethodMultiplier>();

        public MixingMethodMultiplier GetMultiplier(MixingMethodType mixingMethod)
        {
            int count = mixingMethodMultipliers.Count;
            for (int i = 0; i < count; i++)
            {
                if(mixingMethodMultipliers[i].mixingMethod == mixingMethod)
                {
                    return mixingMethodMultipliers[i];
                }
            }
            return new MixingMethodMultiplier();
        }
    }
}