using System;
using UnityEngine;

namespace Avastrad.UI.MobileSafeArea
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaAdapter: MonoBehaviour
    {
        [SerializeField] private Pair adaptXAnchors = new(true, true);
        [SerializeField] private Pair adaptYAnchors = new(true, true);
        
        private Pair _prevAdaptX;
        private Pair _prevAdaptY;
        private Vector2 _defaultAnchorMin;
        private Vector2 _defaultAnchorMax;
        private RectTransform _rectTransform;
        private ScreenOrientation _prevOrientation;
        private Vector2Int _prevScreenSize;
        private Rect _prevRect;

        private void Awake()
        {
            _prevAdaptX = adaptXAnchors;
            _prevAdaptY = adaptYAnchors;
            
            _rectTransform = GetComponent<RectTransform>();
            _defaultAnchorMin = _rectTransform.anchorMin;
            _defaultAnchorMax = _rectTransform.anchorMax;
            
            _prevOrientation = Screen.orientation;
            _prevScreenSize = new Vector2Int(Screen.width, Screen.height);
            
            AdaptUiForSafeZone();
        }

#if UNITY_EDITOR
        private void FixedUpdate() 
            => AdaptUiForSafeZone();
#endif

        private void AdaptUiForSafeZone()
        {
            var safeZone = Screen.safeArea;
            
            if(safeZone != _prevRect || Screen.width != _prevScreenSize.x || Screen.height != _prevScreenSize.y
                || Screen.orientation != _prevOrientation || _prevAdaptX != adaptXAnchors || _prevAdaptY != adaptYAnchors)
            {
                var anchorMin = safeZone.position;
                var anchorMax = safeZone.position + safeZone.size;

                if (adaptXAnchors.MinAnchor)
                    anchorMin.x /= Screen.width;
                else
                    anchorMin.x = _defaultAnchorMin.x;
                    
                if (adaptXAnchors.MinAnchor)
                    anchorMax.x /= Screen.width;
                else
                    anchorMax.x = _defaultAnchorMax.x;
 
                if (adaptYAnchors.MinAnchor) 
                    anchorMin.y /= Screen.height;
                else
                    anchorMin.y = _defaultAnchorMin.y;

                if (adaptYAnchors.MaxAnchor) 
                    anchorMax.y /= Screen.height;
                else
                    anchorMax.y = _defaultAnchorMax.y;

                _prevAdaptX = adaptXAnchors;
                _prevAdaptY = adaptYAnchors;
                
                _rectTransform.anchorMin = anchorMin;
                _rectTransform.anchorMax = anchorMax;
                
                _prevOrientation = Screen.orientation;
                _prevScreenSize.x = Screen.width;
                _prevScreenSize.y = Screen.height;
                _prevRect = safeZone;
            }
        }
        
        [Serializable]
        private struct Pair
        {
            [field: SerializeField] public bool MinAnchor { get; private set; }
            [field: SerializeField] public bool MaxAnchor { get; private set; }

            public Pair(bool minAnchor, bool maxAnchor)
            {
                MinAnchor = minAnchor;
                MaxAnchor = maxAnchor;
            }

            public static bool operator ==(Pair pairLeft, Pair pairRight) 
                => pairLeft.MinAnchor == pairRight.MinAnchor && pairLeft.MaxAnchor == pairRight.MaxAnchor;

            public static bool operator !=(Pair pairLeft, Pair pairRight) 
                => !(pairLeft == pairRight);
        }
    }
}