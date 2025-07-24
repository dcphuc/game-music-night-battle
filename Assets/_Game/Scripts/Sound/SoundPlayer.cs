using UnityEngine.Assertions;
using UnityEngine;

namespace MusicBattle
{
    public static class SoundPlayer
    {
        private static SoundSystem soundSystem;

        public static void Initialize()
        {
            soundSystem = Object.FindObjectOfType<SoundSystem>();
            Assert.IsNotNull(soundSystem);
        }

        public static void PlaySoundFx(string soundName)
        {
            soundSystem.PlaySoundFx(soundName);
        }

        public static void StopSoundFx(string soundName)
        {
            soundSystem.StopSoundFx(soundName);
        }

        public static void PlayBGM(string soundName)
        {
            soundSystem.PlayBGM(soundName);
        }

        public static void StopBGM(string soundName)
        {
            soundSystem.StopBGM(soundName);
        }
    }
}

