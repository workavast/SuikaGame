using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Elements.MergeProgress
{
    public class MergeProgressCell : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void SetSkin(Sprite newSkin) 
            => image.sprite = newSkin;
    }
}