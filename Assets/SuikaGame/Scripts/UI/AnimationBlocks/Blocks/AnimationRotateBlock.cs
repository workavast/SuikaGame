using System;
using DG.Tweening;
using UnityEngine;

namespace SuikaGame.Scripts.UI.AnimationBlocks.Blocks
{
    [Serializable]
    public class AnimationRotateBlock : IAnimationBlock
    {
        [SerializeField] private RectTransform block;
        [Space] 
        [SerializeField] private int rotationsShowCount;
        [SerializeField, Range(-1, 1)] private int directionShow = 1;
        [SerializeField, Min(0)] private float showDuration = 0.3f;
        [SerializeField] private Ease easeShow = Ease.OutSine;
        [Space]
        [SerializeField] private int rotationsHideCount;
        [SerializeField, Range(-1, 1)] private int directionHide = -1;
        [SerializeField, Min(0)] private float hideDuration = 0.3f;
        [SerializeField] private Ease easeHide = Ease.InSine;
        
        private Tween _tween;
        
        public bool IsActive => _tween.IsActive();
        
        public void Show(Action onCompleted = null)
        {
            if (_tween.IsActive())
                _tween.Kill();
            
            block.rotation = Quaternion.identity;
            block
                .DORotate(new Vector3(0, 0, 360 * directionShow * rotationsShowCount), showDuration, RotateMode.FastBeyond360)
                .SetLink(block.gameObject)
                .SetEase(easeShow)
                .OnKill(() => _tween = null)
                .OnComplete(() =>
                {
                    _tween = null;
                    onCompleted?.Invoke();
                });
        }

        public void Hide(Action onCompleted = null)
        {
            if (_tween.IsActive())
                _tween.Kill();
            
            block.rotation = Quaternion.identity;
            _tween =
                block
                    .DORotate(new Vector3(0, 0, 360 * directionHide * rotationsHideCount), hideDuration, RotateMode.FastBeyond360)
                    .SetLink(block.gameObject)
                    .SetEase(easeHide)
                    .OnKill(() => _tween = null)
                    .OnComplete(() =>
                    {
                        _tween = null;
                        onCompleted?.Invoke();
                    });
        }

        public void HideInstantly()
        {
            if (_tween.IsActive())
                _tween.Kill();
            
            block.rotation = Quaternion.Euler(new Vector3(0, 0, 360 * directionHide * rotationsHideCount));
        }
    }
}