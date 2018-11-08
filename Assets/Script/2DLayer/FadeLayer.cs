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

        public void FadeIn()
        {
            StartCoroutine(FadeEnumerator(1, 0));
        }

        public void FadeOut(Color targeColor)
        {
            targeColor.a = 0;
            image.color = targeColor;
            StartCoroutine(FadeEnumerator(0, 1));
        }

        public void ForceColor(Color color)
        {
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