using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkLordGame
{
    [System.Serializable]
    public class SoundManager
    {
        public AudioClip dayBGM;

        public AudioClip nightBGM;
        public float bubbleVolume = 1;
        public AudioClip bubble;
        public float buyVolume = 0.5f;
        public AudioClip buy;
        public AudioClip magicSound;

        public AudioSource bgm;
        public AudioSource effect;

        public void StopBGM()
        {
            bgm.Stop();
        }

        public void PlayDaySound()
        {
            bgm.clip = dayBGM;
            bgm.PlayDelayed(2);
        }

        public void PlayNightSound()
        {
            bgm.clip = nightBGM;
            bgm.Play();
        }

        public void PlayBubbleSound()
        {
            PlayBubbleSound(1);
        }

        public void PlayBubbleSound(float pictch)
        {
            effect.pitch = pictch;
            effect.volume = bubbleVolume;
            effect.PlayOneShot(bubble);
        }

        public void PlayBuySound()
        {
            effect.volume = buyVolume;
            effect.PlayOneShot(buy);
        }

        public void PlayMagicSound()
        {
            effect.volume = 1;
            effect.PlayOneShot(magicSound);
        }
    }
}