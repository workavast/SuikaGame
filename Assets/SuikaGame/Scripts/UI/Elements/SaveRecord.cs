using SuikaGame.Scripts.Score;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements
{
    [RequireComponent(typeof(Button))]
    public class SaveRecord : MonoBehaviour
    {
        [Inject] private IScoreCounter _scoreCounter;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => _scoreCounter.ApplyRecord());
        }
    }
}