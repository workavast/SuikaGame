using System.Collections.Generic;
using SuikaGame.Scripts.Skins.Entities;
using SuikaGame.Scripts.UI.Elements;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows.Skins.Preview
{
    public class EntitiesSkinPackPreview : MonoBehaviour
    {
        private readonly List<ImageHolder> _entitiesSkinPacksPreviews = new (16);
        
        private void Awake() 
            => _entitiesSkinPacksPreviews.AddRange(GetComponentsInChildren<ImageHolder>());

        public void SetNewSkins(EntitiesSkinPackConfig entitiesSkinPackConfig)
        {
            for (int i = 0; i < entitiesSkinPackConfig.Skins.Count && i < _entitiesSkinPacksPreviews.Count; i++)
                _entitiesSkinPacksPreviews[i].sprite = entitiesSkinPackConfig.Skins[i];
        }
    }
}