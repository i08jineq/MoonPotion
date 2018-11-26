using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "Tutorial", menuName = "DarkLordGame/UnlockData/Tutorial")]
    public class TutorialUnlockData : UnlockData
    {
        public List<string> tutorialMessages = new List<string>();

        public override UnlockDataType GetUnlockType()
        {
            return UnlockDataType.Tutorial;
        }
    }
}