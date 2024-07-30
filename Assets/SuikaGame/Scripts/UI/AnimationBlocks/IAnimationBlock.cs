using System;

namespace SuikaGame.Scripts.UI.AnimationBlocks
{
    public interface IAnimationBlock
    {
        public bool IsActive { get; }

        public void Show(Action onCompleted = null);
        public void Hide(Action onCompleted = null);
        public void HideInstantly();
    }
}