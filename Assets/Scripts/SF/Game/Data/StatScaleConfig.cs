using System.Collections.Generic;
using SF.Game.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Game.Data
{
    [CreateAssetMenu(fileName = "New Stat Scale Config", menuName = "Game/Stats/Scale")]
    public class StatScaleConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<PrimaryStat, List<ScaleStatData>> _scales;

        public IEnumerable<ScaleStatData> GetStatScales(PrimaryStat stat)
        {
            return _scales.ContainsKey(stat) ? _scales[stat] : default(IEnumerable<ScaleStatData>);
        }
    }
}