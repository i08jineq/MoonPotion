using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class DayTimeManager : MonoBehaviour
    {
        public AnimationCurve pointLightintensity;
        public List<Light> pointLights = new List<Light>();
        public Gradient spotLightColor;
        public List<Light> spotLightList = new List<Light>();

        protected const float realtimeSecondPerHour = 5;
        protected float deltaTimeCount = 0;
        protected int currentTimeOfDay = 9;

        protected const int startTimeOfDay = 9;
        protected const int endTimeOfDay = 21;

        public void ResetDay()
        {
            deltaTimeCount = 0;
            currentTimeOfDay = startTimeOfDay;
        }

        public void OnUpdate(float deltaTime)
        {
            deltaTimeCount += deltaTime;
            UpdateTimeOfDay();
            UpdateEnvironment();
            CheckDayEnded();
        }

        protected void UpdateTimeOfDay()
        {
            int timeOfDay = (int)(deltaTimeCount / realtimeSecondPerHour) + startTimeOfDay;
            if(currentTimeOfDay != timeOfDay)
            {
                currentTimeOfDay = timeOfDay;
                Singleton.instance.events.onDayTimeChanged.Invoke(currentTimeOfDay);
            }
        }

        private void UpdateEnvironment()
        {
            float gradientWeight = CalculateTimeWeightdOfDay();
            Color color = spotLightColor.Evaluate(gradientWeight);
            int spotLightNumbers = spotLightList.Count;
            for (int i = 0; i < spotLightNumbers; i++)
            {
                spotLightList[i].color = color;
            }

            float intensity = pointLightintensity.Evaluate(gradientWeight);
            int pointLightNumbers = pointLights.Count;
            for (int i = 0; i < pointLightNumbers; i++)
            {
                pointLights[i].intensity = intensity;
            }
        }

        private float CalculateTimeWeightdOfDay()
        {
            return (float)(deltaTimeCount / realtimeSecondPerHour) / (float)(endTimeOfDay - startTimeOfDay);
        }

        protected void CheckDayEnded()
        {
            if (currentTimeOfDay >= endTimeOfDay)
            {
                Singleton.instance.events.onDayEnded.Invoke();
            }
        }

    }
}
