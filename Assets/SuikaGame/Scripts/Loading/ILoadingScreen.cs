using System;

namespace SuikaGame.Scripts.Loading
{
    public interface ILoadingScreen
    {
        public bool IsShow { get; }

        public event Action FadeAnimationEnded;
        
        public void StartLoading();
        public void EndLoading();
        public void EndLoadingInstantly();
    }
}