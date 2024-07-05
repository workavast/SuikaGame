using UnityEngine;

namespace SuikaGame.Scripts.UI
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private NewRecordTitle newRecordTitle;
        
        private void Start() 
            => Hide();

        public void Show()
        {
            gameObject.SetActive(true);
            newRecordTitle.TryShow();
        }

        public void Hide() 
            => gameObject.SetActive(false);
    }
}