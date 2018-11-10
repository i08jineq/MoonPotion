﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkLordGame
{
    public class GameMain : MonoBehaviour
    {
        public FadeLayer fadeLayer;
        public DayTimeManager day;
        private bool shouldExecuteDay = false;

        #region initialization
        private void Awake()
        {
            fadeLayer.ForceColor(Color.black);
        }

        public IEnumerator Start()
        {
            yield return Singleton.Init();

            fadeLayer.FadeIn();
        }

        private void SetupDay()
        {
            
        }

        #endregion

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            UpdateDay(deltaTime);
        }

        private void UpdateDay(float deltaTime)
        {
            if(shouldExecuteDay)
            {
                day.OnUpdate(deltaTime);
            }
        }
    }
}