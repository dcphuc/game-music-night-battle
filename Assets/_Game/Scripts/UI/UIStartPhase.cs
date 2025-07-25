using UnityEngine;
namespace MusicBattle
{

    public class UIStartPhase : MonoBehaviour
    {
        [SerializeField] private GameObject _goBattle = null;
        [SerializeField] private GameObject _goTryAgain = null;

        private void Start()
        {
            _goBattle.SetActive(true);
            _goTryAgain.SetActive(false);
        }

        public void Setup(bool isTryAgain)
        {
            _goBattle.SetActive(!isTryAgain);
            _goTryAgain.SetActive(isTryAgain);
        }

        public void OnStartBattle()
        {
            GameManager.Instance.UpdatePhase(GamePhase.Countdown);
            SoundPlayer.PlayBGM("bgm");
        }
    }
}
