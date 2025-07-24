using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MusicBattle
{
    public class UIAlertMessItem : MonoBehaviour
    {
        [SerializeField] private Image _img = null;
        [SerializeField] private List<Sprite> _alertSprites = null;

        void OnDisable()
        {
            StopAllCoroutines();
        }

        public void Setup(AlertType alertType)
        {
            transform.localScale = Vector3.one;
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            if (_alertSprites.Count == 0 || _img == null)
            {
                Debug.LogWarning("No alert sprites available or Image component is not assigned.");
                return;
            }

            int index = (int)alertType;
            if (index < 0 || index >= _alertSprites.Count)
            {
                Debug.LogWarning("Alert type index out of range.");
                return;
            }

            _img.sprite = _alertSprites[index];
            _img.SetNativeSize();
            HideAfter1sDelay();
        }

        private void HideAfter1sDelay()
        {
            StartCoroutine(HideCoroutine(1f));
        }

        private IEnumerator HideCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.GetComponent<PooledObject>().Pool.ReturnObject(gameObject);
        }
    }
}
