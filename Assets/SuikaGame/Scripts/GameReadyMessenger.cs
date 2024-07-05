using GamePush;
using UnityEngine;

namespace SuikaGame.Scripts
{
    public class GameReadyMessenger : MonoBehaviour
    {
        private static bool _isMessageSend; 
        
        private void Awake()
        {
            if (_isMessageSend)
            {
                Destroy(gameObject);
                return;
            }
            
            GP_Game.GameReady();
            _isMessageSend = true;
            Destroy(gameObject);
        }
    }
}