using UnityEngine;

namespace SuikaGame.Scripts.UI.Elements
{
    public class NewRecordTitle : MonoBehaviour
    {
        public void Show() 
            => gameObject.SetActive(true);

        public void Hide() 
            => gameObject.SetActive(false);
    }
}