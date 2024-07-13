using System;
using UnityEngine;

namespace SuikaGame.Scripts.CameraSizeAdapting
{
    [RequireComponent(typeof(Camera))]
    public class CameraSizeAdapter : MonoBehaviour
    {
        [SerializeField] private Vector2Int referenceResolution;

        private Camera _camera;
        private float _prevAspect;
        private float _refAspect;
        private float _defaultSize;
        private Vector3 _defaultPosition;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _refAspect = (float)referenceResolution.y / referenceResolution.x;
            _defaultSize = _camera.orthographicSize;
            _defaultPosition = transform.position;
        }

        private void FixedUpdate()
        {
            var curAspect = (float)Screen.height / Screen.width;

            if(Math.Abs(curAspect - _prevAspect) < 0.0001f)
                return;
            
            if (_refAspect < curAspect)
            {
                var newSize = _defaultSize * curAspect / _refAspect;
                _camera.orthographicSize = newSize;

                var newPosition = _defaultPosition + Vector3.up * (newSize - _defaultSize);
                transform.position = newPosition;
            }
            else
            {
                _camera.orthographicSize = _defaultSize;
                transform.position = _defaultPosition;
            }

            _prevAspect = curAspect;
        }
    }
}