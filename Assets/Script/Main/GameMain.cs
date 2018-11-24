using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkLordGame
{
    public class GameMain : MonoBehaviour
    {
        public FadeLayer fadeLayer;
        public DayTimeManager dayManager = new DayTimeManager();
        public NightTimeManager nightTimeManager = new NightTimeManager();
        public GameEventManager gameEventManager = new GameEventManager();
        private bool shouldExecuteDay = false;

        #region initialization

        private void Awake()
        {
            fadeLayer.ForceColor(Color.black);
        }

        public IEnumerator Start()
        {
            yield return Singleton.Init();

            yield return dayManager.SetupStartDayTime();
            yield return nightTimeManager.SetupEnumerator();
            gameEventManager.Setup();
            yield return fadeLayer.FadeIn();

        }

        #endregion

        #region mainLoop

        private void CheckEvent()
        {

        }

        private void StartCrafting()
        {

        }

        private void StartDay()
        {
            StartCoroutine(StartDayEnumerator());
        }

        private IEnumerator StartDayEnumerator()
        {
            yield return dayManager.startDay();
        }

        #endregion

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            UpdateDay(deltaTime);
        }

        private void UpdateDay(float deltaTime)
        {
            if (shouldExecuteDay)
            {
                dayManager.OnUpdate(deltaTime);
            }
        }
    }
}