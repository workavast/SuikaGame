using SuikaGame.Scripts.Skins;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SuikaGame.Scripts.UI.Elements
{
    [RequireComponent(typeof(Button))]
    public class SkinPackButton : MonoBehaviour
    {
        [SerializeField] private SkinPackType skinPackType;
        
        [Inject] private ISkinPackChanger _skinPackChanger;
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(()=> _skinPackChanger.ChangeActiveSkinPack(skinPackType));
        }
    }
}