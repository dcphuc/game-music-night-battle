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

        public Action<ArrowDirection> OnArrowPressed;

        public static GameManager Instance { get; private set; }

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
            GameObject arrowDrop = _arrowPool.GetObject();
            if (arrowDrop != null)
            {
                UIArrowDrop uiArrowDrop = arrowDrop.GetComponent<UIArrowDrop>();
                if (uiArrowDrop != null)
                {
                    uiArrowDrop.Setup();
                }
            }
        }

        private void HandleArrowPressed(ArrowDirection direction)
        {
            Debug.Log($"Arrow pressed: {direction}");
        }
    }
}

