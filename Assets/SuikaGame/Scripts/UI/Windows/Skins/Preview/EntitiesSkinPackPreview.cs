using System.Collections.Generic;
using SuikaGame.Scripts.Skins.Entities;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows.Skins.Preview
{
    public class EntitiesSkinPackPreview : MonoBehaviour
    {
        private readonly List<EntitySkinPreview> _entitiesSkinPacksPreviews = new (16);
        
        private void Awake() 
            => _entitiesSkinPacksPreviews.AddRange(GetComponentsInChildren<EntitySkinPreview>());

        public void SetNewSkins(EntitiesSkinPackConfig entitiesSkinPackConfig)
        {
            for (int i = 0; i < entitiesSkinPackConfig.Skins.Count && i < _entitiesSkinPacksPreviews.Count; i++)
                _entitiesSkinPacksPreviews[i].SetNewSkin(entitiesSkinPackConfig.Skins[i]);
        }
    }
}