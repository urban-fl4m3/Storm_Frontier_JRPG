using System.Collections.Generic;
using SF.Battle.Abilities.Mechanics.Data;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Battle.Abilities
{
    public abstract class BattleAbilityData : SerializedScriptableObject
    {
        [SerializeField] private string _name;
        [OdinSerialize] private IMechanicData[] _mechanicsData;

        public string Name => _name;
        public IEnumerable<IMechanicData> MechanicsData => _mechanicsData;
        
        public abstract bool IsPassive { get; }
    }
}