using System;
using UnityEngine;

namespace SF.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public abstract class BaseMechanicData : IMechanicData
    {
        public MechanicPick Pick => _pick;
        
        [SerializeField] private MechanicPick _pick;
    }
}