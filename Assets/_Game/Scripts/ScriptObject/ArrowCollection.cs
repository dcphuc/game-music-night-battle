using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MusicBattle
{
    [Serializable]
    [CreateAssetMenu(fileName = "ArrowCollection", menuName = "MusicBattle/Arrow Collection", order = 1)]
    public class ArrowCollection : ScriptableObject
    {
        [SerializeField] private List<ArrowType> _greyArrows = new List<ArrowType>();
        [SerializeField] private List<ArrowType> _colorArrows = new List<ArrowType>();

        public ArrowType GetGreyArrowByDirection(ArrowDirection direction)
        {
            foreach (var arrow in _greyArrows)
            {
                if (arrow.Direction == direction)
                {
                    return arrow;
                }
            }
            return null;
        }

        public ArrowType GetColorArrowByDirection(ArrowDirection direction)
        {
            foreach (var arrow in _colorArrows)
            {
                if (arrow.Direction == direction)
                {
                    return arrow;
                }
            }
            return null;
        }
    }

    [Serializable]
    public class ArrowType
    {
        public ArrowDirection Direction;
        public Sprite ArrowSprite;
    }
}

