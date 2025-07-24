using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MusicBattle
{
    public class UIArrowDrop : MonoBehaviour
    {
        [SerializeField] private Image _imgArrow = null;

        public void Setup(ArrowDirection direction, Vector3 target, bool hasColor = false)
        {
            transform.localScale = Vector3.one;
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

            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(localPoint.x, localPoint.y + Screen.height);

            rectTransform.DOLocalMoveY(localPoint.y, 3f);
        }
    }
}

