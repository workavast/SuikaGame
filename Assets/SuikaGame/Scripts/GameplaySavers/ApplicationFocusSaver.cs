using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplaySavers
{
    public class ApplicationFocusSaver : MonoBehaviour
    {
        private IGameplaySaver _gameplaySaver;

        [Inject]
        public void Construct(IGameplaySaver gameplaySaver)
        {
            _gameplaySaver = gameplaySaver;
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                Debug.LogWarning("OnApplicationFocus");
                _gameplaySaver.Save();
            }
        }
    }
}