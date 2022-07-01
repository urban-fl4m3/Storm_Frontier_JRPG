using System;
using UnityEngine;

namespace SF.Battle.Abilities.Mechanics.Data
{
    [Serializable]
    public class HealMechanicData : BaseMechanicData
    {
        [SerializeField] private int _amount;

        public int Amount => _amount;
    }
}