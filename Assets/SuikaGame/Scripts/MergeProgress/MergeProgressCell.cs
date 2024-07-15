using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.MergeProgress
{
    public class MergeProgressCell : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void SetSkin(Sprite newSkin) 
            => image.sprite = newSkin;
    }
}