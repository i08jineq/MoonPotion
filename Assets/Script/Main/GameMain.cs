using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
namespace DarkLordGame
{
    public class GameMain : MonoBehaviour
    {
        public FadeLayer fadeLayer;
        public DayTimeManager dayManager = new DayTimeManager();
        public NightTimeManager nightTimeManager = new NightTimeManager();
        public GameEventManager gameEventManager = new GameEventManager();
        public CustomersManager customersManager = new CustomersManager();
        public SoundManager soundManager = new SoundManager();

        public TopPanelUI topPanelUI;
        public PauseScreenUI pauseScreen;

        private float playSpeed = 1;

        private bool shouldExecuteDay = false;
        private bool isAllCustomerVisited = true;

        private const int townFee = 100;

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
            SetupCustomer();
            UpdateGoldAmount();
            yield return null;
            SetupSound();
            yield return fadeLayer.FadeIn();
            CheckDayEvent();
        }

        private IEnumerator SetupDayManager()
        {
            yield return dayManager.SetupStartDayTime();
            dayManager.onDayEnded.AddListener(OnBecameNight);
            dayManager.onDayStarted.AddListener(OnDayStarted);
        }

        private IEnumerator SetupNightTimeManager()
        {
            yield return nightTimeManager.SetupEnumerator();
            nightTimeManager.onGoldChanged.AddListener(UpdateGoldAmount);
            nightTimeManager.onGoldChanged.AddListener(soundManager.PlayBuySound);
            nightTimeManager.onFinish.AddListener(StartDay);
            nightTimeManager.onCraftedNewItem.AddListener(OnCraftedNewItem);
        }

        private void SetupGameEvent()
        {
            gameEventManager.Setup();
            gameEventManager.onUnlockingNewIngredient.AddListener(OnUnlockingNewIngredient);
            gameEventManager.onChangedShopName.AddListener(OnChangedShopName);
        }

        private void SetupTopPanelUI()
        {
            topPanelUI.SetGoldAmount(Singleton.instance.saveData.currentGold);
            topPanelUI.pauseButton.onClick.AddListener(OnPauseGame);
            topPanelUI.normalSpeedButton.onClick.AddListener(OnPressNormalButton);
            topPanelUI.fastButton.onClick.AddListener(OnPressFastButton);
            topPanelUI.superFastButton.onClick.AddListener(OnPressSuperFastButton);
            topPanelUI.SetPlaySpeedActive(topPanelUI.normalSpeedButton);
            topPanelUI.gameObject.SetActive(true);
            topPanelUI.SetName(Singleton.instance.saveData.shopName);
            UpdateDayUI();
        }

        private void SetupPauseScreen()
        {
            pauseScreen.resumeButton.onClick.AddListener(OnResumeGame);
            pauseScreen.saveAndQuitButton.onClick.AddListener(OnSaveAndQuitGame);
        }

        private void SetupCustomer()
        {
            customersManager.Setup();
            customersManager.onCustomerDonated.AddListener(OnCustomerDonated);
            customersManager.onAllCustomerVisited.AddListener(OnAllCustomerVisited);
        }

        private void SetupSound()
        {
            Button[] buttons = GetComponentsInChildren<Button>(true);
            int count = buttons.Length;
            for (int i = 0; i < count; i++)
            {
                buttons[i].onClick.AddListener(soundManager.PlayBubbleSound);
            }
            soundManager.PlayNightSound();
            Singleton.instance.soundManager = soundManager;
        }

        #endregion

        #region mainLoop

        private void CheckDayEvent()
        {
            gameEventManager.onFinishedAllEvent.AddListener(OnFinishedEvent);
            gameEventManager.TryStartDayEvent(Singleton.instance.saveData.currentDay);
        }

        private void OnFinishedEvent()
        {
            gameEventManager.onFinishedAllEvent.RemoveListener(OnFinishedEvent);
            StartNightTimeSequence();
        }

        private void StartNightTimeSequence()
        {
            nightTimeManager.Start();
        }

        private void StartDay()
        {
            Singleton.instance.saveData.currentDay++;
            UpdateDayUI();
            customersManager.StandbyCustomers();
            soundManager.StopBGM();

            StartCoroutine(dayManager.SunriseEnumerator());
        }

        private void OnDayStarted()
        {
            soundManager.PlayDaySound();
            shouldExecuteDay = true;
            isAllCustomerVisited = false;
        }

        private void OnBecameNight()
        {
            soundManager.PlayNightSound();
            shouldExecuteDay = false;
            TryEndTheDay();
        }

        private void OnAllCustomerVisited()
        {
            isAllCustomerVisited = true;
            TryEndTheDay();
        }

        private void TryEndTheDay()
        {
            bool isDayEnded = (shouldExecuteDay == false && isAllCustomerVisited == true);
            if ( isDayEnded == false)
            {
                return;
            }

            if (PayTownFee())
            {
                Singleton.instance.SaveData();
                CheckDayEvent();
                return;
            }
            StartCoroutine(GameOver());
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
            UpdateCustomer(deltaTime);
        }

        private void UpdateDay(float deltaTime)
        {
            if (shouldExecuteDay)
            {
                dayManager.OnUpdate(deltaTime);
            }
        }

        private void UpdateCustomer(float deltaTime)
        {
            if(isAllCustomerVisited)
            {
                return;
            }
            customersManager.OnUpdate(deltaTime);
        }

        private void UpdateGoldAmount()
        {
            topPanelUI.SetGoldAmount(Singleton.instance.saveData.currentGold);
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

        private void OnCustomerDonated(int gold)
        {
            Singleton.instance.saveData.currentGold += gold;
            soundManager.PlayBuySound();
            UpdateGoldAmount();
        }

        private bool PayTownFee()
        {
            if(Singleton.instance.saveData.currentGold >= townFee)
            {
                Singleton.instance.saveData.currentGold -= townFee;
                Singleton.instance.SaveData();
                soundManager.PlayBuySound();
                UpdateGoldAmount();
                return true;
            }
            soundManager.effect.pitch = 3;
            soundManager.PlayBuySound();
            return false;
        }

        private IEnumerator GameOver()
        {
            Singleton.instance.saveData.isBackroupted = true;
            Singleton.instance.SaveData();

            yield return fadeLayer.FadeOut(Color.black);
            SceneManager.LoadScene("GameOver");
        }

        private void OnCraftedNewItem()
        {
            gameEventManager.TryUnlockIngredient(Singleton.instance.saveData.totalCraft);
        }

        private void UpdateDayUI()
        {
            topPanelUI.SetDay(Singleton.instance.saveData.currentDay);
        }

        private void OnUnlockingNewIngredient()
        {
            soundManager.PlayMagicSound();
        }

        private void OnChangedShopName()
        {
            topPanelUI.SetName(Singleton.instance.saveData.shopName);
        }
    }
}