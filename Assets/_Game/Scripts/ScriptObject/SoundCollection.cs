using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    [CreateAssetMenu(fileName = "SoundCollection", menuName = "Music Battle/Sound collection", order = 1)]
    public class SoundCollection : ScriptableObject
    {
        public List<AudioClip> Sounds;
    }
}


