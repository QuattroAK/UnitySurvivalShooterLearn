using UnityEngine;
using System;
using System.Collections.Generic;

namespace MyGame
{
    public class SoundController : MonoBehaviour
    {
        private static SoundController instance;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioItem> audioItems;

        public static SoundController Instance 
        {
            get
            {
                return instance;
            }
        }

        public void Init()
        {
            instance = this;
        }

        public void PlayAudio(TypeAudio typeAudio)
        {
            foreach (var audioItem in audioItems)
            {
                if (audioItem.TypeAudio == typeAudio)
                {
                    audioSource.clip = audioItem.AudioClip;
                }
            }

            audioSource.Play();
        }
    }

    public enum TypeAudio
    {
        PlayerDamage,
        PlayerDeath,
        GunShot
    }

    [Serializable]
    public class AudioItem
    {
        public TypeAudio TypeAudio;
        public AudioClip AudioClip;
    }
}
