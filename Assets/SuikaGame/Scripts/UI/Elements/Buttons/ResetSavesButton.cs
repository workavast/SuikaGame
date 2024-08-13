using SuikaGame.Scripts.Saves;
using UnityEngine;
using UnityEngine.UI;

namespace SuikaGame.Scripts.UI.Elements.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ResetSavesButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(PlayerData.ResetSave);
        }
    }
}