using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace DarkLordGame
{
    public class HomeMain : MonoBehaviour
    {
        public string gameSceneName = "GameScene";
        public FadeLayer fadeLayer;
        public Button startButton;
        public Button continueButton;
        public Button quitButton;
        public AudioSource effect;
        public AudioClip bubbleSound;
        private void Awake()
        {
            fadeLayer.ForceColor(Color.black);
        }

        private IEnumerator Start()
        {
            yield return Singleton.Init();
            SetupUI();

            yield return fadeLayer.FadeIn();
        }

        private void SetupUI()
        {
            startButton.onClick.AddListener(OnStartNewGame);

            startButton.onClick.AddListener(PlayBubbleSound);

            continueButton.interactable = (Singleton.instance.saveData.currentDay > 0 && Singleton.instance.saveData.isBackroupted == false);
            continueButton.onClick.AddListener(ChangeScene);
            continueButton.onClick.AddListener(PlayBubbleSound);

            quitButton.onClick.AddListener(OnQuitGame);
            quitButton.onClick.AddListener(PlayBubbleSound);
        }

        private void OnStartNewGame()
        {
            Singleton.instance.saveData = new SaveData();
            ChangeScene();
        }

        private void ChangeScene()
        {
            StartCoroutine(FadeCoroutine());
        }

        private void PlayBubbleSound()
        {
            effect.PlayOneShot(bubbleSound);
        }

        private IEnumerator FadeCoroutine()
        {
            yield return fadeLayer.FadeOut(Color.black);
            SceneManager.LoadScene(gameSceneName);
        }

        private void OnQuitGame()
        {
            Application.Quit();
        }
    }
}