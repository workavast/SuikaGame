using System;

namespace SuikaGame.Scripts.UI.Elements.AnimationBlocks
{
    public interface IAnimationBlock
    {
        public bool IsActive { get; }

        public void Show(Action onCompleted = null);
        public void Hide(Action onCompleted = null);
    }
}