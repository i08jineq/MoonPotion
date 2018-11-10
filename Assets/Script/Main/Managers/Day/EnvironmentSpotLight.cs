using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class EnvironmentSpotLight : EnvironmentFillLight
    {
        public AnimationCurve lightAngleCurve;

        /// <summary>
        /// time  0 ~ 1
        /// </summary>
        public override void UpdateTime(float time)
        {
            base.UpdateTime(time);
            Vector3 eurler = transform.eulerAngles;
            float lightAngleY = lightAngleCurve.Evaluate(time);
            eurler.y = lightAngleY;
            transform.eulerAngles = eurler;
        }
    }
}