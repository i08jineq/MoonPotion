using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class DonateRequirement
    {
        public DonateRequirementType donateRequirementType;
        [Range(1, 10)]public float value;
    }
}