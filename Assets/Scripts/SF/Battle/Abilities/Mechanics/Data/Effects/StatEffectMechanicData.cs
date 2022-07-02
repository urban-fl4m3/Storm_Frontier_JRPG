using System;
using SF.Game.Stats;
using UnityEngine;

namespace SF.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public class StatEffectMechanicData : EffectMechanicData
    {
        [SerializeField] private PrimaryStat _stat;
        [SerializeField] private bool _isFlatBoost;
        [SerializeField] private float _statBoostValue;

        public PrimaryStat Stat => _stat;
        public bool IsFlatBoost => _isFlatBoost;
        public float StatBoostValue => _statBoostValue;
    }
}