using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class DayTimeManager
    {
        public List<EnvironmentLight> environmentLights = new List<EnvironmentLight>();
        public Gradient backgroundColor;
        public float turnToDayPeriod = 1;
        protected const float timeCountSpeed = .2f;
        protected float deltaTimeCount = 0;
        protected int currentTimeOfDay = 0;

        protected const int startWorkTimeOfDay = 9;
        protected const float endWorkTimeOfDay = 21;
        protected const float hourOfDay = 24;

        public IEnumerator SetupStartDayTime()
        {
            deltaTimeCount = startWorkTimeOfDay;
            currentTimeOfDay = startWorkTimeOfDay;
            UpdateEnvironment();
            yield break;
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
            deltaTimeCount += deltaTime * timeCountSpeed;
            UpdateTimeOfDay();
            UpdateEnvironment();
            CheckDayEnded();
        }

        protected void UpdateTimeOfDay()
        {
            int timeOfDay = (int)(deltaTimeCount) + startWorkTimeOfDay;
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

            Color bgColor = backgroundColor.Evaluate(weight);
            Camera.main.backgroundColor = bgColor;
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
