using Avastrad.UI.UI_System;
using SuikaGame.Scripts.UI.Elements;
using UnityEngine;

namespace SuikaGame.Scripts.UI.Windows
{
    public class GameplayEndWindow : UI_ScreenBase
    {
        [SerializeField] private NewRecordTitle newRecordTitle;

        public override void Initialize()
            => Hide();

        public override void Show()
        {
            gameObject.SetActive(true);
            newRecordTitle.TryShow();
        }
    }
}