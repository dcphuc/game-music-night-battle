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

            }
            return null;
        }

        public void OnUpArrowPressed()
        {
            GameManager.Instance.OnArrowPressed?.Invoke(ArrowDirection.Up);
        }

        public void OnDownArrowPressed()
        {
            GameManager.Instance.OnArrowPressed?.Invoke(ArrowDirection.Down);
        }

        public void OnLeftArrowPressed()
        {
            GameManager.Instance.OnArrowPressed?.Invoke(ArrowDirection.Left);
        }

        public void OnRightArrowPressed()
        {
            GameManager.Instance.OnArrowPressed?.Invoke(ArrowDirection.Right);
        }
    }
}

