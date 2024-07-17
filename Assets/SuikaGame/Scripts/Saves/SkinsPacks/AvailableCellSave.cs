using System;
using System.Collections.Generic;

namespace SuikaGame.Scripts.Saves.SkinsPacks
{
    [Serializable]
    public class AvailableCellSave<T>
        where T : Enum
    {
        public T Key;
        public bool IsAvailable;

        public AvailableCellSave(T key, bool isAvailable = false)
        {
            Key = key;
            IsAvailable = isAvailable;
        }
        
        public AvailableCellSave(KeyValuePair<T, bool> keyValuePair)
        {
            Key = keyValuePair.Key;
            IsAvailable = keyValuePair.Value;
        }
    }
}