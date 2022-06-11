using System.Collections.Generic;
using SF.Battle.Abilities;
using SF.Game.Stats;
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
        [OdinSerialize] private StatContainerData<PrimaryStat> _additionalPrimaryStats;
        [OdinSerialize] private BattleAbilityData[] _battleAbilitiesData;
        
        public string Name => _name;
        public StatContainerData<MainStat> Tiers => _tiers;
        public StatContainerData<PrimaryStat> AdditionalPrimaryStats => _additionalPrimaryStats;
        public IEnumerable<BattleAbilityData> BattleAbilitiesData => _battleAbilitiesData;
    }
}