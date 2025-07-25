using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MusicBattle
{
    public class UIImageEffect : MonoBehaviour
    {
        [SerializeField] private Image _img = null;
        public Vector3 originScale = new Vector3(0.3f, 1f, 1f);
        public float time = 0.5f;

        Sequence _sequence;

        public void Play()
        {
            gameObject.SetActive(true);
            if (_sequence != null)
            {
                _sequence.Kill();
                _sequence = null;
            }
            transform.localScale = originScale;
            _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 0f);
            _sequence = DOTween.Sequence();
            _sequence.Join(_img.transform.DOScale(Vector3.one, time).SetEase(Ease.OutBack));
            _sequence.Join(_img.DOFade(0.5f, time).SetEase(Ease.Linear));
            _sequence.OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
            _sequence.Play();
        }

        void OnDisable()
        {
            transform.localScale = originScale;
            if (_sequence != null)
            {
                _sequence.Kill();
                _sequence = null;
            }
        }
    }
}
