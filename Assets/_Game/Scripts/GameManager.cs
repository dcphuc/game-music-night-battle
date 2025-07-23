using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ObjectPool _arrowPool = null;
        [SerializeField] private UIButtonArrowPanel _arrowPanel = null;
        [SerializeField] private ArrowCollection _arrowCollection = null;

        private Queue<ArrowDirection> _arrowQueue = new Queue<ArrowDirection>();

        public Action<ArrowDirection> OnArrowPressed;
        public static GameManager Instance { get; private set; }
        public ArrowCollection ArrowCollection => _arrowCollection;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            OnArrowPressed += HandleArrowPressed;
        }

        private void CreateArrowDrop()
        {
            var nextArrow = _arrowPanel.GetNextArrow();
            if (nextArrow != null)
            {
                _arrowQueue.Enqueue(nextArrow.Direction);
            }
            ArrowDirection direction = _arrowQueue.Dequeue();
            GameObject arrowObject = _arrowPool.GetObject();
            arrowObject.SetActive(true);
            OnArrowPressed?.Invoke(direction);
        }

        private void HandleArrowPressed(ArrowDirection direction)
        {
            Debug.Log($"Arrow pressed: {direction}");
        }
    }
}

