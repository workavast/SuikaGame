using SuikaGame.Scripts.Vfx;
using UnityEngine;
using Zenject;

namespace SuikaGame.Scripts.GameplayField.Hiding
{
    public class GameplayFieldVfxHider : MonoBehaviour
    {
        [SerializeField] private float vfxScale;
        
        private IVfxFactory _vfxFactory;
        
        [Inject]
        public void Construct(IVfxFactory vfxFactory) 
            => _vfxFactory = vfxFactory;

        public void HideGameField() 
            => _vfxFactory.Create(VfxType.Smoke, transform.position, vfxScale);
    }
}