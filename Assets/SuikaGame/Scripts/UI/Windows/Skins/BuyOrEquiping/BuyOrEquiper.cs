using System;

namespace SuikaGame.Scripts.UI.Windows.Skins.BuyOrEquiping
{
    public abstract class BuyOrEquiper<T> : IBuyOrEquiper
        where T : Enum
    {
        public T Type { get; private set; }
        public int Price { get; private set; }
        protected BuyOrEquipButton BuyOrEquipButton { get; private set; }

        protected BuyOrEquiper(BuyOrEquipButton buyOrEquipButton)
        {
            BuyOrEquipButton = buyOrEquipButton;
        }
            
        public void Set(T newType)
        {
            Type = newType;
            Price = GetPrice();
            UpdateButtonState();
        }
            
        public abstract void BuyOrEquip();
        
        public abstract void UpdateButtonState();

        protected abstract int GetPrice(); 
    }
}