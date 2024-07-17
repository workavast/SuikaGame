using TMPro;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows.Skins
{
    public class PriceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        
        public void SetPrice(int price) 
            => tmpText.text = price.ToString();

        public void ToggleVisibility(bool isVisible) 
            => gameObject.SetActive(isVisible);
    }
}