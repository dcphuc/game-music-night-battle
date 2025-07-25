using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public class UIArrowButton : MonoBehaviour
    {
        [SerializeField] private ArrowDirection _direction;
        [SerializeField] private UIImageEffect _imageEffect;

        private RectTransform _rectTransform;
        public ArrowDirection Direction => _direction;
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public Vector3 GetPosition()
        {
            var cam = UIManager.Instance.UICamera;
            var screenPoint = cam.WorldToScreenPoint(transform.position);
            return screenPoint;
        }
        public void OnArrowPressed()
        {
            GameManager.Instance.OnArrowPressed?.Invoke(_direction);
        }

        public void PlayImageEffect()
        {
            _imageEffect.Play();
        }
    }
}

