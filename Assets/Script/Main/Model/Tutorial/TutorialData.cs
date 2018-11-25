using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "Tutorial", menuName = "DarkLordGame/Tutorial")]
    public class TutorialData : UnlockData
    {
        public List<string> tutorialMessages = new List<string>();

        public override UnlockDataType GetUnlockType()
        {
            return UnlockDataType.Tutorial;
        }
    }
}