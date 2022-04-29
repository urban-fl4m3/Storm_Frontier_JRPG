﻿using SF.Game.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Game.Characters.Professions
{
    [CreateAssetMenu(fileName = "New Profession Config", menuName = "Game/Configs/Profession")]
    public class ProfessionData : SerializedScriptableObject
    {
        [SerializeField] private string _name;
        [OdinSerialize] private StatContainerData<MainStat> _tiers;

        public string Name => _name;
        public StatContainerData<MainStat> Tiers => _tiers;
    }
}