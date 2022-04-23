using System;
using UnityEngine;

namespace SF.Game.Stats
{
    [Serializable]
    public struct StatData
    {
        public int BaseValue => _baseValue;
        public int Tier => _tier;
        
        [SerializeField] private int _baseValue;
        [SerializeField] private int _tier;

        public StatData(int baseValue, int tier)
        {
            _baseValue = baseValue;
            _tier = tier;
        }
    }
}