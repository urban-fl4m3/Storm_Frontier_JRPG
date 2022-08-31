using System;
using System.Collections.Generic;
using System.Linq;
using SF.Common.Actors.Components.Stats;
using SF.Game.Data;
using SF.Game.Data.Characters;
using SF.Game.Stats;
using UnityEngine;

namespace SF.Battle.Stats
{
    public class StatContainer
    {
        public event Action<MainStat> MainStatChanged;
        public event Action<PrimaryStat> PrimaryStatChanged;
        
        private readonly int _level;
        
        private readonly StatScaleConfig _statScaleConfig;
        private readonly IStatUpgradeFormula _statUpgradeFormula = new DefaultStatUpgradeFormula();
        
        private readonly Dictionary<MainStat, int> _mainStats = new();
        private readonly Dictionary<PrimaryStat, int> _primaryStats = new();
        private readonly Dictionary<PrimaryStat, PrimaryStatResource> _resourceStats = new();

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

        public void SetStatValue(MainStat stat, int value)
        {
            _mainStats[stat] = value;
            
            MainStatChanged?.Invoke(stat);
        }

        public void AddStatValue(MainStat stat, int value)
        {
            var currentStatValue = _mainStats[stat];
            _mainStats[stat] = currentStatValue + value;
            
            MainStatChanged?.Invoke(stat);
        }

        public void SetStatValue(PrimaryStat stat, int value)
        {
            _primaryStats[stat] = value;
    
            UpdatePrimaryStatResource(stat, value);
            PrimaryStatChanged?.Invoke(stat);
        }

        public void AddStatValue(PrimaryStat stat, int value)
        {
            var currentStatValue = _primaryStats[stat];
            var newValue = currentStatValue + value;
            _primaryStats[stat] = newValue;
            
            UpdatePrimaryStatResource(stat, newValue);
            PrimaryStatChanged?.Invoke(stat);
        }

        public IPrimaryStatResource GetStatResourceResolver(PrimaryStat stat)
        {
            if (!_resourceStats.TryGetValue(stat, out var resourceStat))
            {
                resourceStat = new PrimaryStatResource(stat);
                _resourceStats.Add(stat, resourceStat);
            }

            return resourceStat;
        }

        private void UpdatePrimaryStatResource(PrimaryStat stat, int value)
        {
            if (_resourceStats.ContainsKey(stat))
            {
                _resourceStats[stat].UpdateMaxStat(value);
            }
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
    }
}