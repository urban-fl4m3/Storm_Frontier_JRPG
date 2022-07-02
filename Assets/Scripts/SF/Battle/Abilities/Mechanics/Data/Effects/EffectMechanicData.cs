using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public abstract class EffectMechanicData : BaseMechanicData, IEffectData
    {
        [SerializeField] private bool _isStackable;
        [SerializeField] private bool _hasDuration;
        [SerializeField] [ShowIf(nameof(_hasDuration))] private int _rounds;

        public bool IsStackable => _isStackable;
        public bool HasDuration => _hasDuration;
        public int Rounds => _rounds;
    }
}