using System;
using UnityEngine;

namespace SF.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public class DamageMechanicData : IMechanicData
    {
        [SerializeField] private bool _isFlat;
        [SerializeField] private float _damageBoostPercentage;

        public bool IsFlat => _isFlat;
        public float DamageBoostPercentage => _damageBoostPercentage;
    }
}