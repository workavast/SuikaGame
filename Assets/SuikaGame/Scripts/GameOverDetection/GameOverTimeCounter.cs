using TMPro;
using UnityEngine;

namespace SuikaGame.Scripts.GameOverDetection
{
    public class GameOverTimeCounter : MonoBehaviour
    {
        [SerializeField] private GameOverZone gameOverZone;
        [SerializeField] private TMP_Text leftCounter;
        [SerializeField] private TMP_Text rightCounter;

        private void Awake()
        {
            gameOverZone.OnTimerUpdate += UpdateTimers;
            gameOverZone.OnEnterEntities += Show;
            gameOverZone.OnOutEntities += Hide;
            
            Hide();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false); 
        }
        
        private void UpdateTimers()
        {
            leftCounter.text = rightCounter.text = gameOverZone.CurTime.ToString();
        }
    }
}