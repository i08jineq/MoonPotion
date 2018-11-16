using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class DayTimeManager
    {
        public List<EnvironmentLight> environmentLights = new List<EnvironmentLight>();
        public float turnToDayPeriod = 1;
        protected const float realtimeSecondPerHour = 5;
        protected float deltaTimeCount = 0;
        protected int currentTimeOfDay = 0;

        protected const int startWorkTimeOfDay = 9;
        protected const float endWorkTimeOfDay = 21;
        protected const float hourOfDay = 24;

        public void SetStartDayTime()
        {
            deltaTimeCount = realtimeSecondPerHour * startWorkTimeOfDay;
            currentTimeOfDay = startWorkTimeOfDay;
            UpdateEnvironment();
        }

        public IEnumerator startDay()
        {
            float t = 0;
            while(t <= turnToDayPeriod)
            {
                t += Time.deltaTime;
                deltaTimeCount = Mathf.Lerp(0, startWorkTimeOfDay, t / turnToDayPeriod);
                UpdateTimeOfDay();
                UpdateEnvironment();
                yield return null;
            }
            deltaTimeCount = startWorkTimeOfDay;
            UpdateTimeOfDay();
            UpdateEnvironment();
            Singleton.instance.events.onDayStarted.Invoke();
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
            int timeOfDay = (int)(deltaTimeCount / realtimeSecondPerHour) + startWorkTimeOfDay;
            if(currentTimeOfDay != timeOfDay)
            {
                currentTimeOfDay = timeOfDay;
                Singleton.instance.events.onDayTimeChanged.Invoke(currentTimeOfDay);
            }
        }

        private void UpdateEnvironment()
        {
            int number = environmentLights.Count;
            float weight = deltaTimeCount / hourOfDay;
            for (int i = 0; i < number; i++)
            {
                environmentLights[i].UpdateTime(weight);
            }
        }

        protected void CheckDayEnded()
        {
            if (currentTimeOfDay >= endWorkTimeOfDay)
            {
                Singleton.instance.events.onDayEnded.Invoke();
            }
        }

    }
}
