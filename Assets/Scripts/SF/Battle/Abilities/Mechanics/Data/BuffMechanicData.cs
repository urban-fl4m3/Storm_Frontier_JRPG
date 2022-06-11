using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public abstract class BuffMechanicData : IMechanicData
    {
        [SerializeField] private bool _hasDuration;
        [SerializeField] [ShowIf(nameof(_hasDuration))] private int _rounds;

        public bool HasDuration => _hasDuration;
        public int Rounds => _rounds;
    }
}