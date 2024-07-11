using System.Collections.Generic;
using SuikaGame.Scripts.Skins;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Skins
{
    public class EntitiesSkinsPreview : MonoBehaviour
    {
        private readonly List<EntitySkinPreview> _entitySkinPreviews = new (16);
        
        private void Awake()
        {
            _entitySkinPreviews.AddRange(GetComponentsInChildren<EntitySkinPreview>());
        }

        public void SetNewSkins(SkinsPackConfig skinsPackConfig)
        {
            for (int i = 0; i < skinsPackConfig.Sprites.Count && i < _entitySkinPreviews.Count; i++)
                _entitySkinPreviews[i].SetNewSkin(skinsPackConfig.Sprites[i]);
        }
    }
}