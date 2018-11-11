using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class EnvironmentLight : MonoBehaviour
    {
        public Light lightObject;
        public Gradient dayColor;
        public AnimationCurve lightIntensityCurve;

        public virtual void UpdateTime(float time)
        {
            Color color = dayColor.Evaluate(time);
            float lightIntensity = lightIntensityCurve.Evaluate(time);
            lightObject.intensity = lightIntensity;
            lightObject.color = color;
        }
    }
}