using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    public class ExtendedButton : Button
    {
        public event Action<PointerEventData> OnPointerUpEvent;
        public event Action<PointerEventData> OnPointerDownEvent;
        
        public event Action<PointerEventData> OnPointerEnterEvent;
        public event Action<PointerEventData> OnPointerExitEvent;
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnPointerUpEvent?.Invoke(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnPointerDownEvent?.Invoke(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnPointerEnterEvent?.Invoke(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            OnPointerExitEvent?.Invoke(eventData);
        }
    }
}