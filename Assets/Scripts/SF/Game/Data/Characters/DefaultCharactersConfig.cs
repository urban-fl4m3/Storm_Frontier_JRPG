using SF.Game.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Game.Data.Characters
{
    [CreateAssetMenu(fileName = "New Default Characters Config", menuName = "Game/Base Character")]
    public class DefaultCharactersConfig : SerializedScriptableObject
    {
        [OdinSerialize] private StatContainerData<MainStat> _baseMainStats;
        [OdinSerialize] private StatContainerData<PrimaryStat> _basePrimaryStats;

        public StatContainerData<MainStat> BaseMainStats => _baseMainStats;
        public StatContainerData<PrimaryStat> BasePrimaryStats => _basePrimaryStats;
    }
}