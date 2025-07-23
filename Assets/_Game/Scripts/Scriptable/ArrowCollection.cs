using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    [CreateAssetMenu(fileName = "ArrowCollection", menuName = "MusicBattle/Arrow Collection", order = 1)]
    public class ArrowCollection : ScriptableObject
    {
        public List<ArrowType> Arrows;
    }

    public class ArrowType
    {

    }
}

