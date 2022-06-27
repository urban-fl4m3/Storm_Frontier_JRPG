using System;
using UnityEngine;

namespace SF.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public class HealMechanicData : BaseMechanicData
    {
        [SerializeField] private bool _isFlat;
        [SerializeField] private int _healAmount;

        public bool IsFlat => _isFlat;
        public int HealAmount => _healAmount;
    }
}