using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DarkLordGame
{
    public class FadeLayer : MonoBehaviour
    {
        public Image image;
        public float fadePeriod = 0.3f;

        public IEnumerator FadeIn()
        {
            image.transform.SetAsLastSibling();
            gameObject.SetActive(true);
            yield return FadeEnumerator(1, 0);
            gameObject.SetActive(false);
        }

        public IEnumerator FadeOut(Color targeColor)
        {
            image.transform.SetAsLastSibling();
            gameObject.SetActive(true);
            targeColor.a = 0;
            image.color = targeColor;
            yield return FadeEnumerator(0, 1);
        }

        public void ForceColor(Color color)
        {
            image.transform.SetAsLastSibling();
            image.color = Color.black;
        }

        private IEnumerator FadeEnumerator(float start, float end)
        {
            float t = 0;
            while(t <= fadePeriod)
            {
                t += Time.deltaTime;
                Color currentColor = image.color;
                currentColor.a = Mathf.Lerp(start, end, t);
                image.color = currentColor;
                yield return null;
            }
            Color endColor = image.color;
            endColor.a = end;
            image.color = endColor;
        }
    }
}