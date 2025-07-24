using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundFX : MonoBehaviour
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
                Invoke(nameof(KillSoundFx), clip.length + 0.1f);
            }
        }

        private void KillSoundFx()
        {
            GetComponent<PooledObject>().Pool.ReturnObject(gameObject);
        }
    }
}


