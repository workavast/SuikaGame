using System;
using SuikaGame.Scripts.InputDetection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuikaGame.Scripts.UI
{
    public class InteractableZone : MonoBehaviour, IInput, IPointerEnterHandler, IPointerExitHandler, 
        IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private bool _pointerIsIn;

        public bool IsHold { get; private set; }
        public Vector2 HoldPoint { get; private set; }
        
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
            HoldPoint = Camera.main.ScreenToWorldPoint(spawnPoint);
            IsHold = true;
            Pressed?.Invoke(HoldPoint);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if(!_pointerIsIn)
                return;

            var spawnPoint = eventData.position;
            HoldPoint = Camera.main.ScreenToWorldPoint(spawnPoint);
            IsHold = false;
            Release?.Invoke(HoldPoint);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var spawnPoint = eventData.position;
            HoldPoint = Camera.main.ScreenToWorldPoint(spawnPoint);
            Hold?.Invoke(HoldPoint);
        }
    }
}