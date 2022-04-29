using System;
using System.Collections.Generic;
using System.Linq;
using SF.Common.Logger;
using SF.Game.Data;
using SF.Game.Data.Characters;
using SF.Game.Stats;
using UnityEngine;

namespace SF.Battle.Stats
{
    public class StatContainer: IReadOnlyStatContainer<MainStat>, IReadOnlyStatContainer<PrimaryStat>
    {
        private readonly int _level;
        
        private readonly StatScaleConfig _statScaleConfig;
        private readonly IStatUpgradeFormula _statUpgradeFormula = new DefaultStatUpgradeFormula();
        
        private readonly Dictionary<MainStat, int> _mainStats = new Dictionary<MainStat, int>();
        private readonly Dictionary<PrimaryStat, int> _primaryStats = new Dictionary<PrimaryStat, int>();

        public StatContainer(int level,
            DefaultCharactersConfig defaultCharactersConfig,
            StatContainerData<MainStat> characterAdditionalMainStats, 
            StatContainerData<MainStat> professionMainStatTiers,  
            StatContainerData<PrimaryStat> primaryAdditionalStats,
            StatScaleConfig statScaleConfig)
        {
            _level = level;
            _statScaleConfig = statScaleConfig;

            CalculateMainStats(defaultCharactersConfig.BaseMainStats, characterAdditionalMainStats, professionMainStatTiers);
            CalculatePrimaryStats(defaultCharactersConfig.BasePrimaryStats, primaryAdditionalStats);
        }

        public int GetStat(MainStat stat)
        {
            return _mainStats.ContainsKey(stat) ? _mainStats[stat] : default;
        }

        public int GetStat(PrimaryStat stat)
        {
            return  _primaryStats.ContainsKey(stat) ? _primaryStats[stat] : default;
        }
        
        private void CalculateMainStats(
            StatContainerData<MainStat> defaultCharacterMainStats, 
            StatContainerData<MainStat> characterAdditionalMainStats,
            StatContainerData<MainStat> professionMainStatTiers)
        {
            var defaultStats = defaultCharacterMainStats.Stats;
            var additionalStats = characterAdditionalMainStats.Stats;
            var professionTierStasts = professionMainStatTiers.Stats;
            
            foreach (MainStat stat in Enum.GetValues(typeof(MainStat)))
            {
                var defaultValue = defaultStats.ContainsKey(stat) ? defaultStats[stat] : default;
                var additionalValue = additionalStats.ContainsKey(stat) ? additionalStats[stat] : default;
                
                var totalMainStatValue = Mathf.FloorToInt(defaultValue + additionalValue);
                
                if (professionTierStasts.ContainsKey(stat))
                {
                    var tier = professionTierStasts[stat];   
                    totalMainStatValue = _statUpgradeFormula.GetStatValue(totalMainStatValue, tier, _level);
                }
                
                _mainStats.Add(stat, totalMainStatValue);
            }
        }

        private void CalculatePrimaryStats(
            StatContainerData<PrimaryStat> defaultCharacterPrimaryStatsContainer,
            StatContainerData<PrimaryStat> primaryAdditionalStatsContainer)
        {
            var defaultStats = defaultCharacterPrimaryStatsContainer.Stats;
            var primaryAdditionalStats = primaryAdditionalStatsContainer.Stats;

            foreach (PrimaryStat stat in Enum.GetValues(typeof(PrimaryStat)))
            {
                var totalStatValue = .0f;

                if (defaultStats.ContainsKey(stat))
                {
                    totalStatValue += defaultStats[stat];
                }
               
                if (primaryAdditionalStats.ContainsKey(stat))
                {
                    totalStatValue += primaryAdditionalStats[stat];
                }
                
                var scaleData = _statScaleConfig.GetStatScales(stat);

                if (scaleData != null)
                {
                    var scaledStatValue = GetScaledPrimaryStat(scaleData);
                    totalStatValue += scaledStatValue;
                }

                _primaryStats.Add(stat, Mathf.FloorToInt(totalStatValue));
            }
        }

        private float GetScaledPrimaryStat(IEnumerable<ScaleStatData> scales)
        {
            return scales.Sum(scale => GetStat(scale.Stat) * scale.Value);
        }
        
        private void SetStat(MainStat stat, int value)
        {
            _mainStats[stat] = value;
        }
    }
}