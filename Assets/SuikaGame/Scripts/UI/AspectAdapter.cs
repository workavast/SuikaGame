using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class AspectAdapter : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private RectTransform _canvasRect;
        private float _prevAspect = -1;
        private float _defaultAspect;
        private float _defaultWidth;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _defaultWidth = _rectTransform.rect.width;

            var canvasScaler = GetComponentInParent<CanvasScaler>();
            _defaultAspect = canvasScaler.referenceResolution.y / canvasScaler.referenceResolution.x;
            _canvasRect = canvasScaler.GetComponent<RectTransform>();
        }

        private void Update()
            => TryFixSize();

        private void TryFixSize()
        {
            var currentAspect = (float)Screen.height / Screen.width;
            if (Math.Abs(_prevAspect - currentAspect) < 0.00001f)
                return;
            
            _prevAspect = currentAspect;
            if (1 > currentAspect || currentAspect > _defaultAspect)
                _rectTransform.sizeDelta = new Vector2(_defaultWidth, 0);
            else
                _rectTransform.sizeDelta = new Vector2(_canvasRect.sizeDelta.x, 0);
        }
    }
}