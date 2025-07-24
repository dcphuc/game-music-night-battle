using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _goGamePlay = null;
        [SerializeField] private GameObject _goStartPhase = null;
        [SerializeField] private GameObject _goCountdown = null;
        public Camera UICamera;
        public RectTransform SafeArea;
        public static UIManager Instance { get; private set; }

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

        public void ShowStartPhase(bool isTryAgain = false)
        {
            _goGamePlay.SetActive(false);
            _goCountdown.gameObject.SetActive(false);
            _goStartPhase.SetActive(true);
            _goStartPhase.GetComponent<UIStartPhase>().Setup(isTryAgain);
        }

        public void ShowGamePlay()
        {
            _goStartPhase.SetActive(false);
            _goCountdown.gameObject.SetActive(false);
            _goGamePlay.SetActive(true);
        }

        public void ShowCountdown()
        {
            _goStartPhase.SetActive(false);
            _goCountdown.SetActive(true);
            _goGamePlay.SetActive(true);
            _goCountdown.GetComponent<UICountdown>().StartCountdown();
        }
    }
}

