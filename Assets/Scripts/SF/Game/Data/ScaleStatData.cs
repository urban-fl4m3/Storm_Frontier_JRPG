using System;
using SF.Game.Stats;
using UnityEngine;

namespace SF.Game.Data
{
    [Serializable]
    public class ScaleStatData
    {
        [SerializeField] private MainStat _stat;
        [SerializeField] private float _value;

        public MainStat Stat => _stat;
        public float Value => _value;
    }
}