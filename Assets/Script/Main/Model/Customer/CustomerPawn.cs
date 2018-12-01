using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class CustomerPawn : MonoBehaviour
    {
        public DonateRequirement donateRequirement = new DonateRequirement();
        public int minDonates = 5;
        public int maxDonates = 1000;
        private Transform localTransform;

        public void Setup()
        {
            localTransform = transform;
        }

        public void SetTransform(Vector3 position, Vector3 forward)
        {
            localTransform.position = position;
            localTransform.forward = forward;
        }
    }

}