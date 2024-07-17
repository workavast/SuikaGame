using TMPro;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows.Skins
{
    public class BuyOrEquipButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text price;
        [SerializeField] private GameObject buyBlock;
        [SerializeField] private GameObject equipBlock;
        
        public void SetBuyState(int newPrice)
        {
            price.text = newPrice.ToString();
            buyBlock.SetActive(true);
            equipBlock.SetActive(false);
        }

        public void SetEquipState()
        {
            buyBlock.SetActive(false);
            equipBlock.SetActive(true);
        }
    }
}