using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class DayTimeManager
    {
        public List<EnvironmentLight> environmentLights = new List<EnvironmentLight>();
        private int environmentLightNumbers = 0;
        public Gradient backgroundColor;

        private Color startDayBackgroundColor;
        private Color nightTimeBackgroundColor;

        public float turnToDayPeriod = 1;
        protected float deltaTimeCount = 0;

        protected const float timeCountSpeed = .2f;
        protected const int startWorkTimeOfDay = 0;
        protected const int endWorkTimeOfDay = 10;

        public Communicator onDayStarted = new Communicator();
        public Communicator onDayEnded = new Communicator();
        public Communicator<float> onDayTimeChanged = new Communicator<float>();
        public Communicator onInteruptedByLevelEvent = new Communicator();
        public Communicator onLeventEventEnded = new Communicator();

        #region setup
        public IEnumerator SetupStartDayTime()
        {
            yield return null;
            SetupEnivornmentLight();
            yield return null;
            SetupBackgroundColor();

            deltaTimeCount = endWorkTimeOfDay;
            UpdateEnvironment();
        }

        private void SetupEnivornmentLight()
        {
            environmentLightNumbers = environmentLights.Count;
            for (int i = 0; i < environmentLightNumbers; i++)
            {
                environmentLights[i].Setup();
            }
        }

        private void SetupBackgroundColor()
        {
            startDayBackgroundColor = backgroundColor.Evaluate(0);
            nightTimeBackgroundColor = backgroundColor.Evaluate(1);
        }
        #endregion

        #region event
        public IEnumerator SunriseEnumerator()
        {
            float t = 0;
            while (t <= turnToDayPeriod)
            {
                t += Time.deltaTime;
                UpdateSunriseEnvironment(t / turnToDayPeriod);
                yield return null;
            }
            UpdateSunriseEnvironment(1);
            deltaTimeCount = startWorkTimeOfDay;
            onDayStarted.Invoke();
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
            onDayTimeChanged.Invoke(deltaTimeCount);
        }

        private void UpdateSunriseEnvironment(float weight)
        {

            for (int i = 0; i < environmentLightNumbers; i++)
            {
                environmentLights[i].Sunrise(weight);
            }
            Camera.main.backgroundColor = Color.Lerp(nightTimeBackgroundColor, startDayBackgroundColor, weight);
        }

        private void UpdateEnvironment()
        {
            float weight = deltaTimeCount / endWorkTimeOfDay;
            for (int i = 0; i < environmentLightNumbers; i++)
            {
                environmentLights[i].UpdateTime(weight);
            }

            Color bgColor = backgroundColor.Evaluate(weight);
            Camera.main.backgroundColor = bgColor;
        }

        protected void CheckDayEnded()
        {
            if (deltaTimeCount >= endWorkTimeOfDay)
            {
                onDayEnded.Invoke();
            }
        }
        #endregion
    }
}
