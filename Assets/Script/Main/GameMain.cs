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
        private float playSpeed = 1;
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
            UpdateGoldAmount();
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
            nightTimeManager.onGoldChanged.AddListener(UpdateGoldAmount);
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
            topPanelUI.normalSpeedButton.onClick.AddListener(OnPressNormalButton);
            topPanelUI.fastButton.onClick.AddListener(OnPressFastButton);
            topPanelUI.superFastButton.onClick.AddListener(OnPressSuperFastButton);
            topPanelUI.SetPlaySpeedActive(topPanelUI.normalSpeedButton);
            topPanelUI.gameObject.SetActive(true);
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
            Singleton.instance.SaveData();
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
            Singleton.instance.SaveData();
            Application.Quit();
        }

        #endregion

        private void Update()
        {
            float deltaTime = Time.deltaTime * playSpeed;
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

        private void OnPressNormalButton()
        {
            playSpeed = 1;
            topPanelUI.SetPlaySpeedActive(topPanelUI.normalSpeedButton);
        }

        private void OnPressFastButton()
        {
            playSpeed = 3;
            topPanelUI.SetPlaySpeedActive(topPanelUI.fastButton);
        }

        private void OnPressSuperFastButton()
        {
            playSpeed = 5;
            topPanelUI.SetPlaySpeedActive(topPanelUI.superFastButton);
        }
    }
}