using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicBattle
{
    public class UIAlertMessManager : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool = null;
        public static UIAlertMessManager Instance { get; private set; }
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

        public void ShowAlert(AlertType alertType)
        {
            Debug.Log($"ShowAlert: {alertType}");
            var obj = _objectPool.GetObject();
            var alertItem = obj.GetComponent<UIAlertMessItem>();
            if (alertItem != null)
            {
                alertItem.Setup(alertType);
            }
        }
    }
}

