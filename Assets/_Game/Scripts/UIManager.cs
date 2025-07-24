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

        private void InactivateAll()
        {
            _goGamePlay.SetActive(false);
            _goStartPhase.SetActive(false);
            _goCountdown.gameObject.SetActive(false);
        }
        public void ShowStartPhase()
        {
            InactivateAll();
            _goStartPhase.SetActive(true);
        }

        public void ShowGamePlay()
        {
            InactivateAll();
            _goGamePlay.SetActive(true);
        }

        public void ShowCountdown()
        {
            InactivateAll();
            _goCountdown.SetActive(true);
            _goCountdown.GetComponent<UICountdown>().StartCountdown();
        }
    }
}

