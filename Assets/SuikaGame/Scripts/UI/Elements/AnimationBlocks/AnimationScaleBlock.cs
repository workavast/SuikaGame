using System;
using DG.Tweening;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Elements.AnimationBlocks
{
    [Serializable]
    public class AnimationScaleBlock : IAnimationBlock
    {
        [SerializeField] private RectTransform block;
        [Space]
        [SerializeField, Min(0)] private float showScale = 1f;
        [SerializeField, Min(0)] private float showDuration = 0.75f;
        [SerializeField] private Ease easeShow = Ease.OutBack;
        [Space]
        [SerializeField, Min(0)] private float hideScale = 0;
        [SerializeField, Min(0)] private float hideDuration = 0.75f;
        [SerializeField] private Ease easeHide = Ease.InBack;
        
        private Tween _tween;
        
        public bool IsActive => _tween.IsActive();

        public void Show(Action onCompleted = null)
        {
            if (_tween.IsActive())
                _tween.Kill();

            var durationPercentage = 1f;
            var dif = showScale - hideScale;
            if (dif != 0) 
                durationPercentage = (showScale - block.localScale.x) / dif;
            
            _tween = block
                    .DOScale(showScale, showDuration * durationPercentage)
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
            
            var durationPercentage = 1f;
            var dif = showScale - hideScale;
            if (dif != 0) 
                durationPercentage = (block.localScale.x - hideScale) / dif;

            _tween = block
                    .DOScale(hideScale, hideDuration * durationPercentage)
                    .SetLink(block.gameObject)
                    .SetEase(easeHide)
                    .OnKill(() => _tween = null)
                    .OnComplete(() =>
                    {
                        _tween = null;
                        onCompleted?.Invoke();
                    });
        }
    }
}