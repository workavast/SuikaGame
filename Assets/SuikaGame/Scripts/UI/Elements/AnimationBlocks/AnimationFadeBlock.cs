using System;
using DG.Tweening;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Elements.AnimationBlocks
{
    [Serializable]
    public class AnimationFadeBlock : IAnimationBlock
    {
        [SerializeField] private CanvasGroup block;
        [Space]
        [SerializeField, Range(0, 1)] private float showAlpha = 1f;
        [SerializeField] private float showDuration = 0.3f;
        [SerializeField] private Ease easeShow = Ease.OutSine;
        [Space]
        [SerializeField, Range(0, 1)] private float hideAlpha = 0f;
        [SerializeField] private float hideDuration = 0.3f;
        [SerializeField] private Ease easeHide = Ease.InSine;

        private Tween _tween;
        
        public bool IsActive => _tween.IsActive();
        
        public void Show(Action onCompleted = null)
        {
            if (_tween.IsActive())
                _tween.Kill();

            var durationPercentage = 1f;
            var dif = showAlpha - hideAlpha;
            if (dif != 0) 
                durationPercentage -= (block.alpha - hideAlpha) / dif;
            
            _tween = block
                    .DOFade(showAlpha, showDuration * durationPercentage)
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
            var dif = showAlpha - hideAlpha;
            if (dif != 0) 
                durationPercentage = (block.alpha - hideAlpha) / dif;
            _tween = block
                    .DOFade(hideAlpha, hideDuration * durationPercentage)
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