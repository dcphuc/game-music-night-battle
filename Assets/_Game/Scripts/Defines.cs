using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public enum ArrowDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public class ArrowDropInfo
    {
        public ArrowDirection Direction;
        public GameObject ArrowObject;
    }
}


