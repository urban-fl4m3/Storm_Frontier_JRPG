using System.Collections.Generic;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.TargetSelection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Battle.Abilities
{
    public abstract class BattleAbilityData : SerializedScriptableObject
    {
        [SerializeField] private string _name;
        [OdinSerialize] private IMechanicData[] _mechanicsData;
        [SerializeField] private TargetPick _targetPick;

        public string Name => _name;
        public IEnumerable<IMechanicData> MechanicsData => _mechanicsData;
        public TargetPick Pick => _targetPick;
        
        public abstract bool IsPassive { get; }
    }
}