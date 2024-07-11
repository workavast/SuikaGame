using UnityEngine;

namespace SuikaGame.Scripts.CoroutineHolding
{
    public class CoroutineHolder : MonoBehaviour
    {
        public static CoroutineHolder Instance { get; private set; }

        public void Initialize()
        {
            if (Instance != null)
            {
                Debug.LogError("Duplicate");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}