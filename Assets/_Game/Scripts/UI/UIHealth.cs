using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] private RectTransform _rtOpponentHealth = null;
        [SerializeField] private RectTransform _rtPlayerHealth = null;

        public void ResetHealth()
        {
            _rtOpponentHealth.gameObject.SetActive(true);
            _rtPlayerHealth.gameObject.SetActive(true);
            var width = GetComponent<RectTransform>().rect.width;
            _rtOpponentHealth.sizeDelta = new Vector2(width / 2, _rtOpponentHealth.sizeDelta.y);
            _rtPlayerHealth.sizeDelta = new Vector2(width / 2, _rtPlayerHealth.sizeDelta.y);
        }

        public void UpdateHealth(int playerHealth)
        {
            if (playerHealth < 3 || playerHealth > (Defines.MaxHealth * 2 - 3))
                return;
            var widthPerUnit = GetComponent<RectTransform>().rect.width / (Defines.MaxHealth * 2);
            _rtPlayerHealth.sizeDelta = new Vector2(widthPerUnit * playerHealth, _rtPlayerHealth.sizeDelta.y);
            _rtOpponentHealth.sizeDelta = new Vector2(widthPerUnit * (Defines.MaxHealth * 2 - playerHealth), _rtOpponentHealth.sizeDelta.y);
            if (playerHealth <= 0)
            {
                _rtPlayerHealth.gameObject.SetActive(false);
            }
            else if (playerHealth >= Defines.MaxHealth * 2)
            {
                _rtOpponentHealth.gameObject.SetActive(false);
            }
        }
    }
}

