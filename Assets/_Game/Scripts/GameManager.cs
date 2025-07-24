using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public enum GamePhase
    {
        Start,
        Countdown,
        WaitingForPlayer,
        Playing,
        End
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ObjectPool _arrowPool = null;
        [SerializeField] private UIButtonArrowPanel _arrowButtonPanel = null;
        [SerializeField] private ArrowCollection _arrowCollection = null;

        private Queue<ArrowDropInfo> _arrowQueue = new Queue<ArrowDropInfo>();
        private float _dropTimer = 0f;
        private float _dropInterval = 1f;
        private GamePhase _state = GamePhase.Start;

        public Action<ArrowDirection> OnArrowPressed;
        public static GameManager Instance { get; private set; }
        public ArrowCollection ArrowCollection => _arrowCollection;

        public GamePhase State
        {
            get => _state;
            set => _state = value;
        }

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
            UpdatePhase(GamePhase.Start);
        }

        private void Update()
        {
            _dropTimer += Time.deltaTime;
            if (_dropTimer > _dropInterval)
            {
                CreateArrowDrop();
                _dropTimer = 0f;
            }
        }

        #region  Core Logic
        private void CreateArrowDrop()
        {
            var nextArrow = _arrowButtonPanel.GetNextArrow();
            if (nextArrow != null)
            {
                GameObject arrowObject = _arrowPool.GetObject();
                var uiArrowDrop = arrowObject.GetComponent<UIArrowDrop>();
                if (uiArrowDrop != null)
                {
                    uiArrowDrop.Setup(nextArrow.Direction, nextArrow.TargetPosition, hasColor: true);
                    _arrowQueue.Enqueue(new ArrowDropInfo
                    {
                        Direction = nextArrow.Direction,
                        ArrowObject = arrowObject
                    });
                }
                else
                    Debug.LogError("UIArrowDrop component not found on the arrow object.");
            }
        }

        private void HandleArrowPressed(ArrowDirection direction)
        {
            if (_arrowQueue.Count > 0)
            {
                ArrowDropInfo firstItem = _arrowQueue.Dequeue();
                if (firstItem.Direction == direction)
                {
                    Debug.Log($"Correct arrow pressed: {direction}");
                    firstItem.ArrowObject.SetActive(false); // Deactivate or return to pool
                }
                else
                {
                    Debug.LogWarning($"Incorrect arrow pressed: {direction}, expected: {firstItem.Direction}");
                }
            }
            else
            {
                Debug.LogWarning("No arrows in queue to match the pressed direction.");
            }
        }
        #endregion

        public void UpdatePhase(GamePhase newPhase)
        {
            _state = newPhase;
            switch (_state)
            {
                case GamePhase.Start:
                    UIManager.Instance.ShowStartPhase();
                    break;
                case GamePhase.Countdown:
                    UIManager.Instance.ShowCountdown();
                    break;
                case GamePhase.WaitingForPlayer:
                    UIManager.Instance.ShowGamePlay();
                    break;
                case GamePhase.Playing:
                    UIManager.Instance.ShowGamePlay();
                    break;
                case GamePhase.End:
                    // Logic for ending the game
                    break;
            }
        }
    }
}

