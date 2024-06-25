using UnityEngine;

namespace SuikaGame.Scripts.Vfx
{
    public interface IVfxFactory
    {
        public VfxHolder Create(VfxType vfxType, Vector2 position, float scale);
    }
}