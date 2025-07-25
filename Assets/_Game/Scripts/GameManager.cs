using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
        [SerializeField] private UIHealth _uiHealth = null;

        [SerializeField] private Character _opponentCharacter = null;
        [SerializeField] private Character _playerCharacter = null;

        private Queue<GameObject> _leftArrowQueue = new Queue<GameObject>();
        private Queue<GameObject> _rightArrowQueue = new Queue<GameObject>();
        private Queue<GameObject> _upArrowQueue = new Queue<GameObject>();
        private Queue<GameObject> _downArrowQueue = new Queue<GameObject>();
        private float _dropTimer = 0f;
        private float _dropInterval = 0.5f;
        private int _opponentArrowCount = Defines.OpponentArrowCount;
        private int _playerHealth = Defines.MaxHealth;
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
            SoundPlayer.Initialize();
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
        private void ResetGame()
        {
            _leftArrowQueue.Clear();
            _rightArrowQueue.Clear();
            _upArrowQueue.Clear();
            _downArrowQueue.Clear();
            _dropTimer = 0f;
            _opponentArrowCount = Defines.OpponentArrowCount;
            _playerHealth = Defines.MaxHealth;
            _uiHealth.ResetHealth();
            _opponentCharacter.ResetCharacter();
            _playerCharacter.ResetCharacter();
            _arrowPool.ReturnAll();
        }

        private void DropArrow()
        {
            if (_state == GamePhase.Opponent)
            {
                CreateArrowDrop(hasColor: false);
                _opponentArrowCount--;
                _opponentCharacter.ChangeCharacterSprite();
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
                            UpdateHealthPlayer(type);
                            DequeueArrow(direction);
                            _arrowButtonPanel.PlayFXButton(direction);
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
                            UpdateHealthPlayer(type);
                            DequeueArrow(direction);
                            _arrowButtonPanel.PlayFXButton(direction);
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
                            UpdateHealthPlayer(type);
                            DequeueArrow(direction);
                            _arrowButtonPanel.PlayFXButton(direction);
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
                            UpdateHealthPlayer(type);
                            DequeueArrow(direction);
                            _arrowButtonPanel.PlayFXButton(direction);
                        }
                    }
                    break;
            }
        }

        public void UpdateHealthPlayer(CollectType collectType)
        {
            switch (collectType)
            {
                case CollectType.Missed:
                    OnPlayerHealthChange(-3);
                    break;
                case CollectType.Bad:
                    UIAlertMessManager.Instance.ShowAlert(AlertType.Bad);
                    OnPlayerHealthChange(0);
                    break;
                case CollectType.Sick:
                    UIAlertMessManager.Instance.ShowAlert(AlertType.Sick);
                    OnPlayerHealthChange(1);
                    break;
                case CollectType.Good:
                    UIAlertMessManager.Instance.ShowAlert(AlertType.Good);
                    OnPlayerHealthChange(2);
                    break;
            }
        }

        private void OnPlayerHealthChange(int amount)
        {
            if (_state != GamePhase.Playing)
                return;
            if (amount > 0)
                _playerCharacter.ChangeCharacterSprite();
            _playerHealth += amount;
            _playerHealth = Mathf.Clamp(_playerHealth, 0, Defines.MaxHealth * 2);
            _uiHealth.UpdateHealth(_playerHealth);
            if (_playerHealth <= 0)
                UpdatePhase(GamePhase.End);
            else if (_playerHealth >= Defines.MaxHealth * 2)
                UpdatePhase(GamePhase.End);
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
                    ResetGame();
                    if (_isAttractMode)
                    {
                        PlayAttractMode();
                    }
                    break;
                case GamePhase.Countdown:
                    _opponentCharacter.AutoPlay = false;
                    UIManager.Instance.ShowCountdown();
                    break;
                case GamePhase.Opponent:
                    UIManager.Instance.ShowGamePlay();
                    break;
                case GamePhase.Playing:
                    UIManager.Instance.ShowGamePlay();
                    break;
                case GamePhase.End:
                    SoundPlayer.StopBGM("bgm");
                    UIManager.Instance.ShowStartPhase(true);
                    ResetGame();
                    break;
            }
        }

        // This is a demo function to simulate the attract mode
        private bool _isAttractMode = true;
        private void PlayAttractMode()
        {
            if (_isAttractMode)
            {
                _opponentCharacter.AutoPlay = true;
            }
        }
    }
}

