using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MusicBattle
{
    public class UIArrowDrop : MonoBehaviour
    {
        [SerializeField] private Image _imgArrow = null;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Setup(ArrowDirection direction, float screenPositionX, bool hasColor = false)
        {
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


        }

        private void Update()
        {

            //_rectTransform.anchoredPosition = new Vector2(screenPositionX, _rectTransform.anchoredPosition.y);
        }
    }
}

