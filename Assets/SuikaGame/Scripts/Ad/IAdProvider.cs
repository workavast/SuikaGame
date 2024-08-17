using System;

namespace SuikaGame.Scripts.Ad
{
    public interface IAdProvider
    {
        public event Action OnAdStart;
        public event Action OnAdClose;
    }
}