using System;
using UnityEngine;

namespace SF.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public class DamageMechanicData : BaseMechanicData
    {
        [SerializeField] private bool _isFlat;
        [SerializeField] private float _amount;

        public bool IsFlat => _isFlat;
        public float Amount => _amount;
    }
}