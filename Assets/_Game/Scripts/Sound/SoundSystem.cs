using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public class SoundSystem : MonoBehaviour
    {
        [SerializeField] private ObjectPool _soundFxPool = null;
        [SerializeField] private ObjectPool _bgmPool = null;

        public SoundCollection Collections;
        private readonly Dictionary<string, AudioClip> nameToSound = new Dictionary<string, AudioClip>();

        private void Awake()
        {
            foreach (var sound in Collections.Sounds)
                nameToSound.Add(sound.name, sound);
        }

        private void Start()
        {
            _soundFxPool.Initialize();
            _bgmPool.Initialize();
        }

        #region SFX
        public void PlaySoundFx(string soundName)
        {
            var clip = nameToSound[soundName];
            if (clip != null)
                PlaySoundFx(clip);
        }

        private void PlaySoundFx(AudioClip clip)
        {
            if (clip != null)
                _soundFxPool.GetObject().GetComponent<SoundFX>().Play(clip);
        }

        public void StopSoundFx(string soundName)
        {
            foreach (var sound in _soundFxPool.GetComponentsInChildren<SoundFX>())
            {
                if (sound.GetComponent<AudioSource>().clip == nameToSound[soundName])
                    sound.GetComponent<PooledObject>().Pool.ReturnObject(sound.gameObject);
            }
        }
        #endregion

        #region BGM
        public void PlayBGM(string soundName)
        {
            var clip = nameToSound[soundName];
            if (clip != null)
                PlayBGM(clip);
        }
        private void PlayBGM(AudioClip clip)
        {
            if (clip != null)
                _bgmPool.GetObject().GetComponent<BGM>().Play(clip);
        }

        public void StopBGM(string soundName)
        {
            foreach (var sound in _bgmPool.GetComponentsInChildren<BGM>())
            {
                if (sound.GetComponent<AudioSource>().clip == nameToSound[soundName])
                    sound.GetComponent<PooledObject>().Pool.ReturnObject(sound.gameObject);
            }
        }
        #endregion
    }
}

