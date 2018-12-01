using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
namespace DarkLordGame
{
    public class GameOverScene : MonoBehaviour
    {
        public FadeLayer fadeLayer;
        public Button button;
        public string homeSceneName = "HomeScene";

        private void Awake()
        {
            fadeLayer.ForceColor(Color.black);
        }
        // Use this for initialization
        IEnumerator Start()
        {
            button.gameObject.SetActive(false);
            SetupButton();
            yield return fadeLayer.FadeIn();
            button.gameObject.SetActive(true);
        }

        private void SetupButton()
        {
            button.onClick.AddListener(OnClickScreen);
        }

        public void OnClickScreen()
        {
            StartCoroutine(FadeoutEnumerator());
        }

        private IEnumerator FadeoutEnumerator()
        {
            yield return fadeLayer.FadeOut(Color.black);
            SceneManager.LoadScene(homeSceneName);
        }
    }
}