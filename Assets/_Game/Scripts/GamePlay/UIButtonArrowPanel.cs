using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public class NextArrow
    {
        public ArrowDirection Direction;
        public float ScreenPositionX;
    }

    public class UIButtonArrowPanel : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _listButtons = new List<GameObject>();

        public NextArrow GetNextArrow()
        {
            if (_listButtons.Count > 0)
            {
                var randomIndex = Random.Range(0, _listButtons.Count);
                var nextArrow = _listButtons[randomIndex];
                return new NextArrow
                {
                    Direction = nextArrow.GetComponent<UIArrowButton>().Direction,
                    ScreenPositionX = nextArrow.transform.position.x
                };
            }
            return null;
        }
    }
}

