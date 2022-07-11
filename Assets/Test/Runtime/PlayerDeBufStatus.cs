using System;
using System.Collections.Generic;

namespace Test.Runtime
{
    [Serializable]
    public struct PlayerDeBufStatus
    {
        public List<DeBufTypes> CurrentDeBufTypesList;
        
        public PlayerDeBufStatus AddDeBuf(DeBufTypes deBufTypes)
        {
            CurrentDeBufTypesList ??= new List<DeBufTypes>();
            
            if(!CurrentDeBufTypesList.Contains(deBufTypes))
            {
                CurrentDeBufTypesList.Add(deBufTypes);
            }

            return this;
        }
    }
}