using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    [RequireComponent(typeof(AudioSource))]
    public class BGM : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}

