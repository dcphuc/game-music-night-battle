using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace MusicBattle
{
    public class UIArrowDrop : MonoBehaviour
    {
        [SerializeField] private Image _imgArrow = null;
        private Tweener _tweener;
        private RectTransform _rectTransform;

        private ArrowDirection _arrowDirection;
        private float _targetPosY;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        private void OnDisable()
        {
            if (_tweener != null && _tweener.IsActive())
            {
                _tweener.Kill();
            }
        }

        public void Setup(ArrowDirection direction, Vector3 target, bool hasColor = false)
        {
            transform.localScale = Vector3.one;
            _arrowDirection = direction;
            ArrowType arrowType = hasColor
                ? GameManager.Instance.ArrowCollection.GetColorArrowByDirection(direction)
                : GameManager.Instance.ArrowCollection.GetGreyArrowByDirection(direction);

            if (arrowType != null)
            {
                _imgArrow.sprite = arrowType.ArrowSprite;
            }
            else
            {
                Debug.LogWarning($"No arrow found for direction: {direction}");
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                 UIManager.Instance.SafeArea,
                target,
                UIManager.Instance.UICamera,
                out Vector2 localPoint
            );
            _targetPosY = localPoint.y;
            _rectTransform.anchoredPosition = new Vector2(localPoint.x, localPoint.y + 1920f);

            if (_tweener != null && _tweener.IsActive())
            {
                _tweener.Kill();
            }
            _tweener = _rectTransform.DOLocalMoveY(hasColor ? _targetPosY - 200f : _targetPosY, 1.5f).
            SetEase(Ease.Linear).
            OnComplete(() =>
            {
                GameManager.Instance.DequeueArrow(_arrowDirection);
                GameManager.Instance.UpdateHealthPlayer(collectType: CollectType.Missed);
            });
        }

        public CollectType CheckCollectAvailable()
        {
            float offset = Mathf.Abs(_targetPosY - _rectTransform.anchoredPosition.y);

            if (offset < 0f)
                return CollectType.Missed;

            if (offset < CollectType.Good.GetHashCode())
                return CollectType.Good;
            if (offset < CollectType.Sick.GetHashCode())
                return CollectType.Sick;
            if (offset < CollectType.Bad.GetHashCode())
                return CollectType.Bad;
            return CollectType.None;
        }
    }
}

