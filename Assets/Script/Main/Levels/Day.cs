using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    public class Day
    {
        protected const float secondPerDay = 60;
        protected float dayTimeCount = 0;
        protected int currentTimeOfDay = 9;

        protected const int startTimeOfDay = 9;

        public void Start()
        {
            dayTimeCount = 0;
            currentTimeOfDay = startTimeOfDay;
            Model.instance.gameEvent.onDayTimeChanged.Invoke(currentTimeOfDay);
        }

        public void OnUpdate(float deltaTime)
        {
            dayTimeCount += deltaTime;
            UpdateTimeOfDay();
            if(dayTimeCount >= secondPerDay)
            {
                Model.instance.gameEvent.onDayEnded.Invoke();
            }
        }

        protected void UpdateTimeOfDay()
        {
            int timeOfDay = (int)dayTimeCount / 5 + startTimeOfDay;
            if(currentTimeOfDay != timeOfDay)
            {
                currentTimeOfDay = timeOfDay;
                Model.instance.gameEvent.onDayTimeChanged.Invoke(currentTimeOfDay);
            }
        }
    }
}
