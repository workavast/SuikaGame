using System;

namespace SuikaGame.Scripts.UI.AnimationBlocks
{
    public class AnimationBlocksHolder
    {
        private readonly IAnimationBlock[] _blocks;
        private int _counter;
        private Action _callback;

        public event Action OnAllBlocksShow;
        public event Action OnAllBlocksHide;
        
        public AnimationBlocksHolder(IAnimationBlock[] blocks)
        {
            _blocks = blocks;
        }

        public void Show(Action callback = null)
        {
            _counter = 0;
            _callback = callback;
            foreach (var block in _blocks) 
                block.Show(ShowCounter);
        }

        public void HideInstantly()
        {
            _counter = 0;
            _callback = null;
            foreach (var block in _blocks) 
                block.HideInstantly();
        }
        
        public void Hide(Action callback = null)
        {
            _counter = 0;
            _callback = callback;
            foreach (var block in _blocks) 
                block.Hide(HideCounter);
        }

        private void ShowCounter()
        {
            _counter++;
            if (_counter == _blocks.Length)
            {
                _callback?.Invoke();
                OnAllBlocksShow?.Invoke();
            }
        }

        private void HideCounter()
        {
            _counter++;
            if (_counter == _blocks.Length)
            {
                _callback?.Invoke();
                OnAllBlocksHide?.Invoke();
            }
        }
    }
}