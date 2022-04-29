using SF.Battle.Common;
using SF.Battle.Stats;
using SF.Common.Actors;
using SF.Game;
using SF.Game.Characters.Professions;
using SF.Game.Data;
using SF.Game.Stats;

namespace SF.Battle.Actors
{
    public class BattleActor : Actor
    {
        public Team Team { get; private set; }
        public int Level { get; private set; }

        public IReadOnlyStatContainer<MainStat> MainStats => _stats;
        public IReadOnlyStatContainer<PrimaryStat> PrimaryStats => _stats;
        
        private StatContainer _stats;
        private ProfessionData _professionData;

        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData, StatScaleConfig statScaleConfig)
        {
            Init(serviceLocator);

            Team = metaData.Team;
            Level = metaData.Info.Level;

            _professionData = metaData.Info.Config.ProfessionData;

            var characterData = metaData.Info.Config;
            
            _stats = new StatContainer(Level,
                characterData.BaseMainStats, 
                characterData.ProfessionData.Tiers, 
                characterData.AdditionalPrimaryStats, 
                statScaleConfig,
                ServiceLocator.Logger);


            // ServiceLocator.Logger.Log($"{MainStat.Vitality}: {MainStats.GetStat(MainStat.Vitality)}");
            // ServiceLocator.Logger.Log($"{MainStat.Intelligence}: {MainStats.GetStat(MainStat.Intelligence)}");
            // ServiceLocator.Logger.Log($"{MainStat.Mastery}: {MainStats.GetStat(MainStat.Mastery)}");
            // ServiceLocator.Logger.Log($"{MainStat.Wisdom}: {MainStats.GetStat(MainStat.Wisdom)}");
            // ServiceLocator.Logger.Log($"{MainStat.Focus}: {MainStats.GetStat(MainStat.Focus)}");
            // ServiceLocator.Logger.Log($"{MainStat.Resistance}: {MainStats.GetStat(MainStat.Resistance)}");
            // ServiceLocator.Logger.Log($"{MainStat.Luck}: {MainStats.GetStat(MainStat.Luck)}");
        }
    }
}