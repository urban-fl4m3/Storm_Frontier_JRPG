using SF.Battle.Common;
using SF.Battle.Stats;
using SF.Common.Actors;
using SF.Game;
using SF.Game.Characters.Professions;
using SF.Game.Stats;

namespace SF.Battle.Actors
{
    public class BattleActor : Actor
    {
        public Team Team { get; private set; }
        public int Level { get; private set; }

        public IReadOnlyStatContainer<PrimaryStat> Stats => _stats;
        
        private StatContainer<PrimaryStat> _stats;
        private ProfessionData _professionData;

        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData)
        {
            Init(serviceLocator);

            Team = metaData.Team;
            Level = metaData.Info.Level;

            _professionData = metaData.Info.Config.ProfessionData;
            _stats = new StatContainer<PrimaryStat>(_professionData.Stats, Level);
        }
    }
}