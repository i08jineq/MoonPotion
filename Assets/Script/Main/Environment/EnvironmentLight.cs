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

        private Color startDayColor = Color.white;
        private Color nightTimeColor = Color.black;

        private float startDayIntensity = 1;
        private float nightTimeIntensity = 0;

        public void Setup()
        {
            startDayColor = dayColor.Evaluate(0); // day
            nightTimeColor = dayColor.Evaluate(1); //night
            startDayIntensity = lightIntensityCurve.Evaluate(0);
            nightTimeIntensity = lightIntensityCurve.Evaluate(1);
        }
        public virtual void UpdateTime(float time)
        {
            Color color = dayColor.Evaluate(time);
            float lightIntensity = lightIntensityCurve.Evaluate(time);
            lightObject.intensity = lightIntensity;
            lightObject.color = color;
        }

        public void Sunrise(float weight)
        {
            lightObject.color = Color.Lerp(nightTimeColor, startDayColor, weight);
            lightObject.intensity = Mathf.Lerp(nightTimeIntensity, startDayIntensity, weight);
        }
    }
}