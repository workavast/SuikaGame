using UnityEngine;

namespace Avastrad.UI.MobileSafeArea
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaAdapter: MonoBehaviour
    {
        [SerializeField] private bool adaptX = true;
        [SerializeField] private bool adaptY = true;

        private bool _prevAdaptX;
        private bool _prevAdaptY;
        
        private Vector2 _defaultAnchorMin;
        private Vector2 _defaultAnchorMax;

        private RectTransform _rectTransform;
        private ScreenOrientation _prevOrientation;
        private Vector2Int _prevScreenSize;
        private Rect _prevRect;

        private void Awake()
        {
            _prevAdaptX = adaptX;
            _prevAdaptY = adaptY;
            
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
                || Screen.orientation != _prevOrientation || _prevAdaptX != adaptX || _prevAdaptY != adaptY)
            {
                var anchorMin = safeZone.position;
                var anchorMax = safeZone.position + safeZone.size;

                if (adaptX)
                {
                    anchorMin.x /= Screen.width;
                    anchorMax.x /= Screen.width;
                }
                else
                {
                    anchorMin.x = _defaultAnchorMin.x;
                    anchorMax.x = _defaultAnchorMax.x;
                }

                if (adaptY)
                {
                    anchorMin.y /= Screen.height;
                    anchorMax.y /= Screen.height;
                }
                else
                {
                    anchorMin.y = _defaultAnchorMin.y;
                    anchorMax.y = _defaultAnchorMax.y;
                }

                _prevAdaptX = adaptX;
                _prevAdaptY = adaptY;
                
                _rectTransform.anchorMin = anchorMin;
                _rectTransform.anchorMax = anchorMax;
                
                _prevOrientation = Screen.orientation;
                _prevScreenSize.x = Screen.width;
                _prevScreenSize.y = Screen.height;
                _prevRect = safeZone;
            }
        }
    }
}