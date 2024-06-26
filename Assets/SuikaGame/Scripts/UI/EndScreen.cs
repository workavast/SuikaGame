using UnityEngine;

namespace SuikaGame.Scripts.UI
{
    public class EndScreen : MonoBehaviour
    {
        private void Start() 
            => Hide();

        public void Show() 
            => gameObject.SetActive(true);

        public void Hide() 
            => gameObject.SetActive(false);
    }
}