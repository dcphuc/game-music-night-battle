using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MusicBattle
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
        [SerializeField] private Image _image = null;

        private int _currentSpriteIndex = 0;

        private void Start()
        {
            ResetCharacter();
        }

        public void ResetCharacter()
        {
            _currentSpriteIndex = 0;
            if (_sprites.Count > 0 && _image != null)
            {
                _image.sprite = _sprites[_currentSpriteIndex];
                _image.SetNativeSize();
            }
        }

        public void ChangeCharacterSprite()
        {
            if (_sprites.Count == 0 || _image == null)
            {
                Debug.LogWarning("No sprites available or Image component is not assigned.");
                return;
            }

            _currentSpriteIndex = (_currentSpriteIndex + 1) % _sprites.Count;
            _image.sprite = _sprites[_currentSpriteIndex];
            _image.SetNativeSize();
        }
    }
}

