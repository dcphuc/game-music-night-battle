using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{

    public static class Defines
    {
        public static int MaxHealth = 20;
        public static int OpponentArrowCount = 10;
    }

    public enum ArrowDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public enum CollectType
    {
        None,
        Missed,
        Bad = 400,
        Sick = 300,
        Good = 200
    }

    public enum AlertType
    {
        Bad,
        Sick,
        Good
    }
}


