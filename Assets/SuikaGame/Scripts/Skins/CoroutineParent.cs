using UnityEngine;

namespace SuikaGame.Scripts.Skins
{
    public class CoroutineParent : MonoBehaviour
    {
        public static CoroutineParent Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}