using UnityEngine;

namespace Avastrad.UI.UI_System
{
    public abstract class UI_ScreenBase : MonoBehaviour
    {
        public virtual void Initialize() {}
        
        public virtual void Show()
            => gameObject.SetActive(true);

        public virtual void Hide()
            => gameObject.SetActive(false);
        
        public virtual void HideInstantly()
            => gameObject.SetActive(false);
    }
}