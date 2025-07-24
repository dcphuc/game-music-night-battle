using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public class NextArrow
    {
        public ArrowDirection Direction;
        public Vector3 TargetPosition;
    }

    public class UIButtonArrowPanel : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _listButtons = new List<GameObject>();

        public NextArrow GetNextArrow()
        {
            if (_listButtons.Count > 0)
            {
                var randomIndex = Random.Range(0, _listButtons.Count);
                var nextArrow = _listButtons[randomIndex]?.GetComponent<UIArrowButton>();

                return new NextArrow
                {
                    Direction = nextArrow.Direction,
                    TargetPosition = nextArrow.GetPosition()
                };
            }
            return null;
        }
    }
}

