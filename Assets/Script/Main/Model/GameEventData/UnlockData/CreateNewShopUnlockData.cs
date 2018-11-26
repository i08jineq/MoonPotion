using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [CreateAssetMenu(fileName = "CreateNewShop", menuName = "DarkLordGame/UnlockData/Create new shop")]
    public class CreateNewShopUnlockData : UnlockData
    {
		public override UnlockDataType GetUnlockType()
		{
            return UnlockDataType.CreateNewShop;
		}
	}
}