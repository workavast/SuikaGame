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
        private int _prevHight = -1;
        private int _prevWidth = -1;
        private bool _updateSize;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _defaultWidth = _rectTransform.rect.width;

            var canvasScaler = GetComponentInParent<CanvasScaler>();
            _defaultAspect = canvasScaler.referenceResolution.y / canvasScaler.referenceResolution.x;
            _canvasRect = canvasScaler.GetComponent<RectTransform>();
            _prevHight = Screen.height;
            _prevWidth = Screen.width;
        }

        private void LateUpdate()
            => TryFixSize();

        private void TryFixSize()
        {
            var currentAspect = (float)Screen.height / Screen.width;

            if (_updateSize)
            {
                _prevHight = Screen.height;
                _prevWidth = Screen.width;
                _prevAspect = currentAspect;
                if (1 > currentAspect || currentAspect > _defaultAspect)
                    _rectTransform.sizeDelta = new Vector2(_defaultWidth, 0);
                else
                    _rectTransform.sizeDelta = new Vector2(_canvasRect.sizeDelta.x, 0);

                _updateSize = false;
            }
            else
            {
                if (_prevWidth == Screen.width && _prevHight == Screen.height && Math.Abs(_prevAspect - currentAspect) < 0.00001f)
                    return;

                _updateSize = true;
            }
        }
    }
}