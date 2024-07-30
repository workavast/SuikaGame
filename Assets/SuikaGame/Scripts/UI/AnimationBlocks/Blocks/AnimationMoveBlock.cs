using System;
using DG.Tweening;
using UnityEngine;

namespace SuikaGame.Scripts.UI.AnimationBlocks.Blocks
{
    [Serializable]
    public class AnimationMoveBlock : IAnimationBlock
    {
        [SerializeField] private RectTransform block;
        [Space]
        [SerializeField] private RectTransform showPoint;
        [SerializeField, Min(0)] private float showDuration = 0.3f;
        [SerializeField] private Ease easeShow = Ease.OutSine;
        [Space]
        [SerializeField] private RectTransform hidePoint;
        [SerializeField, Min(0)] private float hideDuration = 0.3f;
        [SerializeField] private Ease easeHide = Ease.InSine;
        
        private Tween _tween;
        
        public bool IsActive => _tween.IsActive();
        
        public void Show(Action onCompleted = null)
        {
            if (_tween.IsActive())
                _tween.Kill();

            var durationPercentage = (showPoint.localPosition - block.localPosition).magnitude / 
                             (showPoint.localPosition - hidePoint.localPosition).magnitude;
            
            _tween = block
                    .DOLocalMove(showPoint.localPosition, showDuration * durationPercentage)
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
            
            var durationPercentage = (block.localPosition - hidePoint.localPosition).magnitude / 
                             (showPoint.localPosition - hidePoint.localPosition).magnitude;
            
            _tween = block
                    .DOLocalMove(hidePoint.localPosition, hideDuration * durationPercentage)
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

            block.localPosition = hidePoint.localPosition;
        }
    }
}