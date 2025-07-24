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
        Opponent,
        Playing,
        End
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ObjectPool _arrowPool = null;
        [SerializeField] private UIButtonArrowPanel _arrowButtonPanel = null;
        [SerializeField] private ArrowCollection _arrowCollection = null;

        private Queue<GameObject> _leftArrowQueue = new Queue<GameObject>();
        private Queue<GameObject> _rightArrowQueue = new Queue<GameObject>();
        private Queue<GameObject> _upArrowQueue = new Queue<GameObject>();
        private Queue<GameObject> _downArrowQueue = new Queue<GameObject>();
        private float _dropTimer = 0f;
        private float _dropInterval = 1f;
        private int _opponentArrowCount = 10;

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
                DropArrow();
                _dropTimer = 0f;
            }
        }

        #region  Core Logic
        private void DropArrow()
        {
            if (_state == GamePhase.Opponent)
            {
                CreateArrowDrop(hasColor: false);
                _opponentArrowCount--;
                if (_opponentArrowCount <= 0)
                {
                    UpdatePhase(GamePhase.Playing);
                }
            }
            else if (_state == GamePhase.Playing)
            {
                CreateArrowDrop(hasColor: true);
            }
        }

        private void CreateArrowDrop(bool hasColor = false)
        {
            var arrow = _arrowButtonPanel.GetNextArrow();
            if (arrow != null)
            {
                GameObject arrowObject = _arrowPool.GetObject();
                var uiArrowDrop = arrowObject.GetComponent<UIArrowDrop>();
                if (uiArrowDrop != null)
                {
                    uiArrowDrop.Setup(arrow.Direction, arrow.TargetPosition, hasColor: hasColor);
                    switch (arrow.Direction)
                    {
                        case ArrowDirection.Left:
                            _leftArrowQueue.Enqueue(arrowObject);
                            break;
                        case ArrowDirection.Right:
                            _rightArrowQueue.Enqueue(arrowObject);
                            break;
                        case ArrowDirection.Up:
                            _upArrowQueue.Enqueue(arrowObject);
                            break;
                        case ArrowDirection.Down:
                            _downArrowQueue.Enqueue(arrowObject);
                            break;
                    }
                }
                else
                    Debug.LogError("UIArrowDrop component not found on the arrow object.");
            }
        }

        private void HandleArrowPressed(ArrowDirection direction)
        {
            if (_state != GamePhase.Playing)
                return;

            switch (direction)
            {
                case ArrowDirection.Left:
                    if (_leftArrowQueue.Count > 0)
                    {
                        var arrowDrop = _leftArrowQueue.Peek();
                        var type = arrowDrop.GetComponent<UIArrowDrop>().CheckCollectAvailable();
                        if (type != CollectType.None && type != CollectType.Missed)
                        {
                            DequeueArrow(direction);
                        }
                    }
                    break;
                case ArrowDirection.Right:
                    if (_rightArrowQueue.Count > 0)
                    {
                        var arrowDrop = _rightArrowQueue.Peek();
                        var type = arrowDrop.GetComponent<UIArrowDrop>().CheckCollectAvailable();
                        if (type != CollectType.None && type != CollectType.Missed)
                        {
                            DequeueArrow(direction);
                        }
                    }
                    break;
                case ArrowDirection.Up:
                    if (_upArrowQueue.Count > 0)
                    {
                        var arrowDrop = _upArrowQueue.Peek();
                        var type = arrowDrop.GetComponent<UIArrowDrop>().CheckCollectAvailable();
                        if (type != CollectType.None && type != CollectType.Missed)
                        {
                            DequeueArrow(direction);
                        }
                    }
                    break;
                case ArrowDirection.Down:
                    if (_downArrowQueue.Count > 0)
                    {
                        var arrowDrop = _downArrowQueue.Peek();
                        var type = arrowDrop.GetComponent<UIArrowDrop>().CheckCollectAvailable();
                        if (type != CollectType.None && type != CollectType.Missed)
                        {
                            DequeueArrow(direction);
                        }
                    }
                    break;
            }
        }

        public void DequeueArrow(ArrowDirection direction)
        {
            switch (direction)
            {
                case ArrowDirection.Left:
                    if (_leftArrowQueue.Count > 0)
                    {
                        var arrowObject = _leftArrowQueue.Dequeue();
                        arrowObject.GetComponent<PooledObject>().Pool.ReturnObject(arrowObject);
                    }
                    break;
                case ArrowDirection.Right:
                    if (_rightArrowQueue.Count > 0)
                    {
                        var arrowObject = _rightArrowQueue.Dequeue();
                        arrowObject.GetComponent<PooledObject>().Pool.ReturnObject(arrowObject);
                    }
                    break;
                case ArrowDirection.Up:
                    if (_upArrowQueue.Count > 0)
                    {
                        var arrowObject = _upArrowQueue.Dequeue();
                        arrowObject.GetComponent<PooledObject>().Pool.ReturnObject(arrowObject);
                    }
                    break;
                case ArrowDirection.Down:
                    if (_downArrowQueue.Count > 0)
                    {
                        var arrowObject = _downArrowQueue.Dequeue();
                        arrowObject.GetComponent<PooledObject>().Pool.ReturnObject(arrowObject);
                    }
                    break;
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
                    _opponentArrowCount = 10;
                    break;
                case GamePhase.Countdown:
                    UIManager.Instance.ShowCountdown();
                    break;
                case GamePhase.Opponent:
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

