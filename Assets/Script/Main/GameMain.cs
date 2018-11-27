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

        public TopPanelUI topPanelUI;
        public PauseScreenUI pauseScreen;

        private bool shouldExecuteDay = false;

        #region initialization

        private void Awake()
        {
            fadeLayer.gameObject.SetActive(true);
            fadeLayer.ForceColor(Color.black);
        }

        public IEnumerator Start()
        {
            yield return Singleton.Init();
            yield return SetupDayManager();
            yield return SetupNightTimeManager();
            SetupGameEvent();
            SetupTopPanelUI();
            SetupPauseScreen();
            yield return fadeLayer.FadeIn();
            CheckEvent();
        }

        private IEnumerator SetupDayManager()
        {
            yield return dayManager.SetupStartDayTime();
            dayManager.onDayEnded.AddListener(onDayEnded);
            dayManager.onDayStarted.AddListener(OnDayStarted);
            dayManager.onDayTimeChanged.AddListener(OnDayTimeChanged);
        }

        private IEnumerator SetupNightTimeManager()
        {
            yield return nightTimeManager.SetupEnumerator();
            nightTimeManager.onFinish.AddListener(StartDay);
        }

        private void SetupGameEvent()
        {
            gameEventManager.Setup();
            gameEventManager.onFinishedAllEvent.AddListener(OnFinishedEvent);
        }

        private void SetupTopPanelUI()
        {
            topPanelUI.SetGoldAmount(Singleton.instance.currentSelectedSaveData.currentGold);
            topPanelUI.pauseButton.onClick.AddListener(OnPauseGame);
        }

        private void SetupPauseScreen()
        {
            pauseScreen.resumeButton.onClick.AddListener(OnResumeGame);
            pauseScreen.saveAndQuitButton.onClick.AddListener(OnSaveAndQuitGame);
        }

        #endregion

        #region mainLoop

        private void CheckEvent()
        {
            gameEventManager.TryStartDayEvent(Singleton.instance.currentSelectedSaveData.currentDay);
        }

        private void OnFinishedEvent()
        {
            StartNightTimeSequence();
        }

        private void StartNightTimeSequence()
        {
            nightTimeManager.Start();
        }

        private void StartDay()
        {
            Singleton.instance.currentSelectedSaveData.currentDay++;
            Singleton.instance.SaveCurrentSlotData();
            StartCoroutine(dayManager.SunriseEnumerator());
        }

        private void OnDayStarted()
        {
            shouldExecuteDay = true;
        }

        private void onDayEnded()
        {
            shouldExecuteDay = false;
        }

        private void OnDayTimeChanged(float timeCount)
        {

        }

        #endregion

        #region event

        private void OnPauseGame()
        {
            Time.timeScale = 0;
            pauseScreen.gameObject.SetActive(true);
        }

        private void OnResumeGame()
        {
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
        }

        private void OnSaveAndQuitGame()
        {
            StartCoroutine(QuitEnumerator());
        }

        private IEnumerator QuitEnumerator()
        {
            Singleton.instance.SaveCurrentSlotData();
            yield return null;
            Application.Quit();
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

        private void UpdateGoldAmount()
        {
            topPanelUI.SetGoldAmount(Singleton.instance.currentSelectedSaveData.currentGold);
        }
    }
}