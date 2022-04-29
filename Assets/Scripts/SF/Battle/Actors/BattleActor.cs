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
                characterData.BaseData,
                characterData.AdditionalMainStats, 
                characterData.ProfessionData.Tiers, 
                characterData.ProfessionData.AdditionalPrimaryStats, 
                statScaleConfig);
        }
    }
}