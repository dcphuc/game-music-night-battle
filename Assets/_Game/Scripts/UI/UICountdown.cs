using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MusicBattle
{
    public class UICountdown : MonoBehaviour
    {
        [SerializeField] private Image _img = null;
        [SerializeField] private List<Sprite> _countdownSprites = null;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void StartCountdown()
        {
            gameObject.SetActive(true);
            StartCoroutine(CountdownCoroutine());
        }

        private IEnumerator CountdownCoroutine()
        {
            int timeLeft = _countdownSprites.Count;
            while (timeLeft > 0)
            {
                _img.sprite = _countdownSprites[timeLeft - 1];
                yield return new WaitForSeconds(1);
                timeLeft--;
            }
            _img.sprite = _countdownSprites[0];
            yield return new WaitForSeconds(1);
            _img.gameObject.SetActive(false);
            GameManager.Instance.UpdatePhase(GamePhase.Opponent);
        }

    }
}
