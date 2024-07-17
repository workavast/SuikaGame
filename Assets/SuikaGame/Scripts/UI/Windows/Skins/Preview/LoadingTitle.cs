using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows.Skins.Preview
{
    public class LoadingTitle : MonoBehaviour
    {
        private int _showRequests;
        
        public void Show()
        {
            _showRequests++;

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _showRequests--;

            if (_showRequests < 0)
            {
                Debug.LogWarning("_showRequests less then zero");
                _showRequests = 0;
            }

            if (_showRequests <= 0) 
                gameObject.SetActive(false);
        }
    }
}