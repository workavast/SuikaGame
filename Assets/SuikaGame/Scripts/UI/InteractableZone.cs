using System;
using SuikaGame.Scripts.InputDetection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuikaGame.UI
{
    public class InteractableZone : MonoBehaviour, IInput, IPointerEnterHandler, IPointerExitHandler, 
        IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private bool _pointerIsIn;
        
        public event Action<Vector2> Pressed;
        public event Action<Vector2> Hold;
        public event Action<Vector2> Release;
        
        public void OnPointerEnter(PointerEventData eventData) 
            => _pointerIsIn = true;

        public void OnPointerExit(PointerEventData eventData) 
            => _pointerIsIn = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            var spawnPoint = eventData.position;
            var gamePoint = Camera.main.ScreenToWorldPoint(spawnPoint);
            Pressed?.Invoke(gamePoint);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if(!_pointerIsIn)
                return;

            var spawnPoint = eventData.position;
            var gamePoint = Camera.main.ScreenToWorldPoint(spawnPoint);
            Release?.Invoke(gamePoint);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var spawnPoint = eventData.position;
            var gamePoint = Camera.main.ScreenToWorldPoint(spawnPoint);
            Hold?.Invoke(gamePoint);
        }
    }
}