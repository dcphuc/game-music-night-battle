using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public class UIArrowButton : MonoBehaviour
    {
        [SerializeField] private ArrowDirection _direction;

        public ArrowDirection Direction => _direction;

        public void OnArrowPressed()
        {
            GameManager.Instance.OnArrowPressed?.Invoke(_direction);
        }
    }
}

