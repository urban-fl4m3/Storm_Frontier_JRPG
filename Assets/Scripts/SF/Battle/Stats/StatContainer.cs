using System.Collections.Generic;
using SF.Common.Logger;
using SF.Game.Data;
using SF.Game.Stats;

namespace SF.Battle.Stats
{
    public class StatContainer: IReadOnlyStatContainer<MainStat>, IReadOnlyStatContainer<PrimaryStat>
    {
        private readonly int _level;
        private readonly StatContainerData<MainStat> _baseMainStats;
        private readonly StatContainerData<MainStat> _professionTiers;
        private readonly StatContainerData<PrimaryStat> _primaryAdditionalStats;
        
        private readonly StatScaleConfig _statScaleConfig;
        private readonly IDebugLogger _debugLogger;
        private readonly IStatUpgradeFormula _statUpgradeFormula = new DefaultStatUpgradeFormula();
        
        private readonly Dictionary<MainStat, int> _mainStats = new Dictionary<MainStat, int>();
        private readonly Dictionary<PrimaryStat, int> _primaryStats = new Dictionary<PrimaryStat, int>();

        public StatContainer(int level, 
            StatContainerData<MainStat> baseMainStats, 
            StatContainerData<MainStat> professionTiers, 
            StatContainerData<PrimaryStat> primaryAdditionalStats,
            StatScaleConfig statScaleConfig,
            IDebugLogger debugLogger)
        {
            _level = level;
            _baseMainStats = baseMainStats;
            _professionTiers = professionTiers;
            _primaryAdditionalStats = primaryAdditionalStats;
            _statScaleConfig = statScaleConfig;
            _debugLogger = debugLogger;

            Recalculate();
        }

        private void Recalculate()
        {
            var professionTierStasts = _professionTiers.Stats;
            
            foreach (var statInfo in _baseMainStats.Stats)
            {
                var stat = statInfo.Key;
                var value = statInfo.Value;

                if (!professionTierStasts.ContainsKey(stat))
                {
                    _debugLogger.LogError($"Stat {stat} doesn't have a tier!");
                    continue;
                }

                var tier = professionTierStasts[stat];
                
                _mainStats.Add(stat, _statUpgradeFormula.GetStatValue(value, tier, _level));
            }   
        }

        public int GetStat(MainStat stat)
        {
            return _mainStats.ContainsKey(stat) ? _mainStats[stat] : default;
        }

        public int GetStat(PrimaryStat stat)
        {
            return 0;
        }
    }
}