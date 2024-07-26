using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    [RequireComponent(typeof(Button))]
    public class SomeButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        
        public event Action OnClick;

        private void Awake() 
            => GetComponent<Button>().onClick.AddListener(() => OnClick?.Invoke());

        public void SetText(string newText)
            => tmpText.text = newText;

        public void ToggleActivity(bool isActive) 
            => gameObject.SetActive(isActive);
    }
}