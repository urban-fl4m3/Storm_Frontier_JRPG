using System;
using System.Collections.Generic;
using SF.Game.Stats;
using UnityEngine;

namespace SF.Battle.Stats
{
    public class StatContainer<TStat> : IReadOnlyStatContainer<TStat> where TStat : Enum
    {
        private readonly IStatUpgradeFormula _statUpgradeFormula = new DefaultStatUpgradeFormula();
        private readonly Dictionary<TStat, int> _statValues = new Dictionary<TStat, int>();

        public StatContainer(IReadOnlyDictionary<TStat, StatData> baseValues, int level)
        {
            ReadBaseStats(baseValues, level);
        }

        private void ReadBaseStats(IReadOnlyDictionary<TStat, StatData> baseValues, int level)
        {
            foreach (var statInfo in baseValues)
            {
                var statType = statInfo.Key;
                var statData = statInfo.Value;

                var upgradedValue =
                    Mathf.FloorToInt(_statUpgradeFormula.GetStatValue(statData.BaseValue, statData.Tier, level));

                _statValues.Add(statType, upgradedValue);
            }
        }

        public int GetStat(TStat stat)
        {
            return _statValues.ContainsKey(stat) ? _statValues[stat] : default;
        }
    }
}